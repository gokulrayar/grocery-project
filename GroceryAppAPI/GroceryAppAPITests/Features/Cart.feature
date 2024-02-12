Feature:Cart

As a registered user, he/she can do the followings - 
1. Can add new items to the cart
2. Can remove items from the cart
3. Can empty the cart

Background:
Given I am a registered user

@cart-retrived-successfully
Scenario:Cart retrieved successfully
When the user sends GET request to the 'users/1/carts' endpoint
Then the response status code should be 200
And the response body should be '{"userId":1,"productIds":[1,2],"id":1}'

@no-cart
Scenario:User does not have any cart
When the user sends GET request to the 'users/2/carts' endpoint
Then the response status code should be 200
And the response body should be '{"message":"User does not have any cart."}'

@cart-retrieve-failed
Scenario:Cart retrived failed user not found
When the user sends GET request to the 'users/12/carts' endpoint
Then the response status code should be 400
And the response body should be '{"message":"User with id 12 is not found."}'

@cart-creation-successfull
Scenario:User creates cart successfully
When the user sends POST request to the 'users/2/carts' endpoint with the data '{"userId":1,"productId":1,"operationType":1}'
Then the response status code should be 200
And the response body should be '{"id":3}'

@cart-creation-failed
Scenario:Cart creation failed due to user not found
When the user sends POST request to the 'users/5/carts' endpoint with the data '{"userId":5,"productId":1}'
Then the response status code should be 400
And the response body should be '{"message":"User with id 5 is not found."}'

@cart-creation-failed
Scenario:Cart creation failed due to product not found
When the user sends POST request to the 'users/2/carts' endpoint with the data '{"userId":1,"productId":0}'
Then the response status code should be 400
And the response body should be '{"message":"Product with id 0 is not found."}'

@cart-updated-successfully
Scenario:One product added to the cart and the cart updated successfully
When the user sends PUT request to the 'users/1/carts/1' endpoint with the data '{"userId":1,"productId":3,"OperationType":1}'
Then the response status code should be 200
And the response body should be '{"message":"Cart updated successfully."}'

@cart-updated-successfully
Scenario:One product deleted from the cart and the cart updated successfully
When the user sends PUT request to the 'users/1/carts/1' endpoint with the data '{"userId":2,"productId":1,"OperationType":2}'
Then the response status code should be 200
And the response body should be '{"message":"Cart updated successfully."}'

@cart-updation-failed
Scenario:Cart updation failed due to cart not found
When the user sends PUT request to the 'users/1/carts/14' endpoint with the data '{"userId":1,"productId":3,"OperationType":1}'
Then the response status code should be 400
And the response body should be '{"message":"Cart with id 14 is not found."}'

@cart-updation-failed
Scenario:Cart updation failed due to user not found
When the user sends PUT request to the 'users/17/carts/3' endpoint with the data '{"userId":17,"productId":3,"OperationType":1}'
Then the response status code should be 400
And the response body should be '{"message":"User with id 17 is not found."}'

@cart-updation-failed
Scenario:Cart updation failed due to product not found
When the user sends PUT request to the 'users/1/carts/1' endpoint with the data '{"userId":1,"productId":18,"OperationType":1}'
Then the response status code should be 400
And the response body should be '{"message":"Product with id 18 is not found."}'

@cart-updation-failed
Scenario:Cart updation failed due to invalid operation type
When the user sends PUT request to the 'users/1/carts/1' endpoint with the data '{"userId":1,"productId":3,"OperationType":3}'
Then the response status code should be 400
And the response body should be '{"message":"OperationType is either not given or invalid."}'

@cart-updation-failed
Scenario:Cart updation failed due to it does not belong to the user
When the user sends PUT request to the 'users/1/carts/2' endpoint with the data '{"userId":1,"productId":3,"OperationType":1}'
Then the response status code should be 400
And the response body should be '{"message":"Cart does not belong to the user."}'

@cart-deletion-successfull
Scenario:Cart deletion successfull
When the user sends DELETE request to the 'users/1/carts/1' endpoint
Then the response status code should be 200
And the response body should be '{"message":"Cart deleted successfully."}'

@cart-deletion-failed
Scenario:Cart deletion failed due to user not found
When the user sends DELETE request to the 'users/20/carts/4' endpoint
Then the response status code should be 400
And the response body should be '{"message":"User with id 20 is not found."}'

@cart-deletion-failed
Scenario:Cart deletion failed due to cart does not belong to the user
When the user sends DELETE request to the 'users/1/carts/2' endpoint
Then the response status code should be 400
And the response body should be '{"message":"Cart does not belong to the user."}'