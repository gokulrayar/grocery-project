Feature:Authentication

As a registered user, he/she tries to login

Background:
Given I am a registered user

@valid-login
Scenario:Registered user login successfully
When the user sends POST request to the 'authentication/login' endpoint with the data '{"username":"testuser@app.com","password":"test123"}'
Then the response status code should be 200
And the response body should be '{"message":"User logged in successfully."}'

@invalid-login
Scenario:Login fails due to wrong username
When the user sends POST request to the 'authentication/login' endpoint with the data '{"username":"xyz@app.com","password":"test123"}'
Then the response status code should be 400
And the response body should be '{"message":"User with the given username not found."}'

@invalid-login
Scenario:Login fails due to wrong password
When the user sends POST request to the 'authentication/login' endpoint with the data '{"username":"testuser@app.com","password":"test456"}'
Then the response status code should be 400
And the response body should be '{"message":"Password is incorrect."}'

@invalid-login
Scenario:Login fails due to blank username
When the user sends POST request to the 'authentication/login' endpoint with the data '{"username":"","password":"test456"}'
Then the response status code should be 400
And the response body should be '{"message":"Username is either not given or invalid."}'

@invalid-login
Scenario:Login fails due to blank password
When the user sends POST request to the 'authentication/login' endpoint with the data '{"username":"testuser@app.com","password":""}'
Then the response status code should be 400
And the response body should be '{"message":"Password is either not given or invalid."}'