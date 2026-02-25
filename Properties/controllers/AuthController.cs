//handles Google login logic

using Microsoft.AspNetCore.Mvc; //Allows creating API controllers
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;//Used to trigger Google login
using System.Security.Claims;//user info from Google
using SSOProject.Data;
using Npgsql;

namespace SSOProject.Controllers
{
    [ApiController]//Marks this as API controller
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly PostgresHelper db = new PostgresHelper();

        // Step 1: Redirect user to Google login
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = "http://localhost:5082/auth/google-response"
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        // Step 2: Handle Google response and save user in PostgreSQL
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            // Get Google user data from claims
            var googleId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            // Safety check
            if (string.IsNullOrEmpty(googleId) || string.IsNullOrEmpty(email))
            {
                return BadRequest("Google authentication failed. Missing user info.");
            }

            using var conn = db.GetConnection();
            await conn.OpenAsync();

            // Step 3: check if user already exists
            var checkCmd = new NpgsqlCommand(
                "SELECT COUNT(*) FROM login WHERE google_id=@gid",
                conn);

            checkCmd.Parameters.AddWithValue("@gid", googleId);

            var count = (long)await checkCmd.ExecuteScalarAsync();

            // Step 4: Insert only if user does not exist
            if (count == 0)
            {
                var insertCmd = new NpgsqlCommand(
                    "INSERT INTO login (google_id, name, email, profile_pic) VALUES (@gid,@name,@email,@pic)",
                    conn);

                insertCmd.Parameters.AddWithValue("@gid", googleId);
                insertCmd.Parameters.AddWithValue("@name", name ?? "");
                insertCmd.Parameters.AddWithValue("@email", email);
                insertCmd.Parameters.AddWithValue("@pic", "");

                await insertCmd.ExecuteNonQueryAsync();
            }

            // Step 5: Return success response
            return Ok(new
            {
                message = "Google login successful",
                googleId = googleId,
                name = name,
                email = email
            });
        }
    }
}