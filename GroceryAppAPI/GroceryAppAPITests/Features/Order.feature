Feature: Order

As a registered user, he/she can do the followings -
1. Can get the order history
2. Can place the order

Background:
Given I am a registered user

@order-history-retrived
Scenario:Order history retrieved successfully
When the user sends GET request to the 'users/1/orders' endpoint
Then the response status code should be 200
And the response body should be '[{"userId":1,"paymentId":1,"orderedAt":"2024-01-15T00:00:00","productIds":[1,2],"id":1},{"userId":1,"paymentId":2,"orderedAt":"2024-01-20T00:00:00","productIds":[1],"id":2}]'

@order-history-retrived-failed
Scenario:Order history retrieve failed due to user not found
When the user sends GET request to the 'users/7/orders' endpoint
Then the response status code should be 400
And the response body should be '{"message":"User with id 7 is not found."}'

@order-placed-successfully
Scenario:Order placed successfully
When the user sends POST request to the 'users/1/orders/payments' endpoint with the data '{"payment":{"amount":3000,"paymentType":1},"order":{ "userId":1,"productIds":[1,2]}}'
Then the response status code should be 200
And the response body should be '{"orderId":3,"paymentId":3}'

@order-placing-failed
Scenario:Order placing failed due to user not found
When the user sends POST request to the 'users/11/orders/payments' endpoint with the data '{"payment":{"amount":3000,"paymentType":1},"order":{ "userId":11,"productIds":[1,2]}}'
Then the response status code should be 400
And the response body should be '{"message":"User with id 11 is not found."}'

@order-placing-failed
Scenario:Order placing failed due to product not found
When the user sends POST request to the 'users/1/orders/payments' endpoint with the data '{"payment":{"amount":3000,"paymentType":1},"order":{ "userId":1,"productIds":[0]}}'
Then the response status code should be 400
And the response body should be '{"message":"Product with id 0 is not found."}'

@order-placing-failed
Scenario:Order placing failed due to no products are passed
When the user sends POST request to the 'users/1/orders/payments' endpoint with the data '{"payment":{"amount":3000,"paymentType":1},"order":{ "userId":1,"productIds":[]}}'
Then the response status code should be 400
And the response body should be '{"message":"ProductIds are either not given or invalid."}'

@order-placing-failed
Scenario:Order placing failed due to invalid payment type
When the user sends POST request to the 'users/1/orders/payments' endpoint with the data '{"payment":{"amount":3000,"paymentType":10},"order":{ "userId":1,"productIds":[1, 2]}}'
Then the response status code should be 400
And the response body should be '{"message":"Payment failed for the order. Order cannot be placed. PaymentType is either not given or invalid."}'

@order-placing-failed
Scenario:Order placing failed due to less payment amount
When the user sends POST request to the 'users/1/orders/payments' endpoint with the data '{"payment":{"amount":2000,"paymentType":1},"order":{ "userId":1,"productIds":[1, 2]}}'
Then the response status code should be 400
And the response body should be '{"message":"Payment failed for the order. Order cannot be placed. Payment amount is less than total amonut of the purchased items."}'