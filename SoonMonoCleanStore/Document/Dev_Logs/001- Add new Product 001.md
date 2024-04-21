## Log 001: Product Management - Adding a New Product - Domain + Unit test

For my side project, I've chosen to develop the "Add New Product" module first. This decision is grounded in a simple rationale: without products to sell, there's no sustenance for my clean store.

Though it's a fundamental CRUD feature, I've opted to incorporate several concepts I've learned along the way. The goal? To craft maintainable code. I've outlined several features for this module and plan to implement them weekly, depending on my availability. So, let's embark on this journey with simple, methodical steps. Below is the feature list:

- Add, update, and delete products.

This particular log is dedicated to the "Add New Product" functionality. For the time being, the requirements are straightforward. The API needs to perform some validations before proceeding with the addition of a new product.

**Details of the Add Product Feature:**

**Pre-Conditions:**
1. Product Name: Must be valid (not empty and not already existing in the database).
2. Price: Must be a positive value.
3. Quantity: Must be greater than 0.

**Action:**
1. Insert the new product into the database, with an auto-generated ID.
2. Record the timestamp and the ID of the user who created this product.

**Post-Action:**
1. Return the newly generated product ID.