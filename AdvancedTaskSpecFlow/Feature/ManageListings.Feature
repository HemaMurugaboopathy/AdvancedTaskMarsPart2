Feature: ManageListingsFeature

As a user, 
I want to login and create and update my listings so that I can manage my listings for skill trade.

@tag1
Scenario Outline: 01 - Edit an existing skills and update with valid deatils 
Given User logged into Mars URL and navigates to Manage Listings tab
When I update my skills in my listings with ID <id>
Then the listings with ID <id> should be updated successfully

 Examples:
    | id |
    | 1  |

Scenario Outline: 02 - View my skills in my listings
    Given User logged into Mars URL and navigates to Manage Listings tab
    When I want to view my skills with ID <id>
    Then I can able to view my skills with ID <id> successfully

    Examples:
    | id |
    | 1  |

Scenario Outline: 03 - Click toggle button to keep active
   Given User logged into Mars URL and navigates to Manage Listings tab
    When I want to click the toggle button to active
    Then the listings with ID <id> should be activated successfully

     Examples:
    | id |
    | 1  |

Scenario Outline: 04 - Click toggle button to keep deactive
   Given User logged into Mars URL and navigates to Manage Listings tab
    When I want to click the toggle button to deactivate
    Then the listings with ID <id> should be deactivated successfully

     Examples:
    | id |
    | 1  |


Scenario Outline: 05 - Delete an existing my skills in my listings
    Given User logged into Mars URL and navigates to Manage Listings tab
    When I want to delete my skills with ID <id>
    Then the skills with ID <id> should be created successfully

     Examples:
    | id |
    | 1  |
