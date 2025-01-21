# Serverless Hello World API

This project demonstrates how to create a serverless API using AWS Lambda and API Gateway. The API responds with a personalized message based on input.

## Features:
- GET `/hello/{name}` - Returns a personalized greeting.
- POST `/hello` - Accepts a JSON payload and returns a greeting.

## How to Deploy:
1. Clone this repository.
2. Set up AWS Lambda and API Gateway using the steps outlined.
3. Deploy and test the API.

## API Examples:
- GET `/hello` - Returns `Hello, World!`.
- GET `/hello/John` - Returns `Hello, John!`.
- POST `/hello` with body `{ "name": "Alice" }` - Returns `Hello, Alice!`.

## Error Handling:
- Returns `400` for missing or incorrect parameters.
- Returns `405` for unsupported HTTP methods.
