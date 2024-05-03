Feature: PasswordFeature

As a user, 
I want to login and change my passowrd so that I can protect my profile.

@tag1
Scenario Outline: 01 - Change password
    Given I logged in Mars portal successfully and navigate to user tab
    When I change my password and update new password with ID <id>
    Then the password with ID <id> should be updated successfully

 Examples:
    | id |
    | 1  |

