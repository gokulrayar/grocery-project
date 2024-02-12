Feature:Product

As an admin user, he/she can do the followings -
1. A new product can be added
2. Can update the existing product details
3. Can remove a product

As a normal registered user, he/she can get all the product details

Background:
Given I am a registered user

@products-retrived
Scenario:All the products retrieved successfully
When the user sends GET request to the 'products' endpoint
Then the response status code should be 200
And the response body should be '[{"name":"Product 1","price":1000,"stock":50,"imageUrl":"www.productimages.com/1","status":1,"id":1},{"name":"Product 2","price":2000,"stock":80,"imageUrl":"www.productimages.com/2","status":1,"id":2},{"name":"Product 3","price":4000,"stock":90,"imageUrl":"www.productimages.com/3","status":1,"id":3}]'

@product-added-successfully
Scenario:Product added successfully
When the user sends POST request to the 'products' endpoint with the data '{"name":"Product 3","price":3000,"stock":70,"imageUrl":"www.productimages.com/3"}'
Then the response status code should be 200
And the response body should be '{"id":3}'

@product-insertion-failed
Scenario:Product insertion failed due to blank product name
When the user sends POST request to the 'products' endpoint with the data '{"name":" ","price":3000,"stock":70,"imageUrl":"www.productimages.com/3"}'
Then the response status code should be 400
And the response body should be '{"message":"Name is either not given or invalid."}'

@product-insertion-failed
Scenario:Product insertion failed due to invalid price
When the user sends POST request to the 'products' endpoint with the data '{"name":"Product 3","price":0,"stock":70,"imageUrl":"www.productimages.com/3"}'
Then the response status code should be 400
And the response body should be '{"message":"Price is either not given or invalid."}'

@product-insertion-failed
Scenario:Product insertion failed due to invalid stock
When the user sends POST request to the 'products' endpoint with the data '{"name":"Product 3","price":3000,"stock":-1,"imageUrl":"www.productimages.com/3"}'
Then the response status code should be 400
And the response body should be '{"message":"Stock is either not given or invalid."}'

@product-updated-successfully
Scenario:Product updated successfully
When the user sends PUT request to the 'products/1' endpoint with the data '{"name":"New Product 1","price":4000,"stock":60,"imageUrl":"www.productimages.com/1"}'
Then the response status code should be 200
And the response body should be '{"message":"Product updated successfully."}'

@product-updation-failed
Scenario:Product updation failed due to product not found
When the user sends PUT request to the 'products/9' endpoint with the data '{"name":"New Product 1","price":4000,"stock":60,"imageUrl":"www.productimages.com/1"}'
Then the response status code should be 400
And the response body should be '{"message":"Product with id 9 is not found."}'

@product-updation-failed
Scenario:Product updation failed due to blank name
When the user sends PUT request to the 'products/1' endpoint with the data '{"name":" ","price":4000,"stock":60,"imageUrl":"www.productimages.com/1"}'
Then the response status code should be 400
And the response body should be '{"message":"Name is either not given or invalid."}'

@product-updation-failed
Scenario:Product updation failed due to invalid price
When the user sends PUT request to the 'products/1' endpoint with the data '{"name":"New Product 1","price":0,"stock":60,"imageUrl":"www.productimages.com/1"}'
Then the response status code should be 400
And the response body should be '{"message":"Price is either not given or invalid."}'

@product-updation-failed
Scenario:Product updation failed due to invalid stock
When the user sends PUT request to the 'products/1' endpoint with the data '{"name":"New Product 1","price":4000,"stock":-1,"imageUrl":"www.productimages.com/1"}'
Then the response status code should be 400
And the response body should be '{"message":"Stock is either not given or invalid."}'

@product-deletion-successfull
Scenario:Product deleted successfully
When the user sends DELETE request to the 'products/1' endpoint
Then the response status code should be 200
And the response body should be '{"message":"Product deleted successfully."}'