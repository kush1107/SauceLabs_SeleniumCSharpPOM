Feature: User Login
  As a registered user
  I want to login to the application
  So that I can access my account

  @smoke @sanity
  Scenario: Successful login with valid credentials
    Given I navigate to the login page
    When I enter the username "tomsmith"
    And I enter the password "SuperSecretPassword!"
    And I click the login button
    Then I should be redirected to the dashboard
    And I should see the welcome message "Welcome to the Secure Area. When you are done click logout below."

  
