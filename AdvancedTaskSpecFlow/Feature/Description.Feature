Feature: DescriptionFeature

As a user, 
I want to login and update my description in my profile page

@tag1
Scenario Outline: 01 - Adding my description in profile page 
Given User logged into Mars URL and navigates to profile page
When I add my description with ID <id>
Then the description with ID <id> should be updated successfully

 Examples:
    | id |
    | 1  |