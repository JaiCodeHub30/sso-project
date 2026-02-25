# SSO Authentication System (ASP.NET Core + Google OAuth + PostgreSQL)

## Overview
This project is a Single Sign-On (SSO) authentication system built using ASP.NET Core Web API. It allows users to securely log in using their Google account and stores user information in a PostgreSQL database.

## Features
- Google OAuth 2.0 Authentication
- Secure user login and registration
- PostgreSQL database integration
- REST API architecture
- Token-based authentication
- Secure credential handling

## Tech Stack
- ASP.NET Core Web API (C#)
- PostgreSQL
- Google OAuth 2.0
- Npgsql
- Git & GitHub

## Project Structure
- Controllers/ → Authentication logic
- data/ → Database helper
- Program.cs → Application startup
- appsettings.json → Configuration

## How it works
1. User clicks Login with Google
2. Google authenticates user
3. Backend receives user info
4. User data stored in PostgreSQL
5. User is logged in securely

## Author
Jai Kumar
GitHub: https://github.com/JaiCodeHub30