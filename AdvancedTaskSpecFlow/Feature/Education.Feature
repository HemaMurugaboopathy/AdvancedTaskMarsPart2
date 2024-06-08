Feature: EducationFeature

As a user, 
I want to login and create and delete my education so that I can manage my education list for skill trade.

@tag1
Scenario Outline: 01 - Delete all records before adding any education
Given I logged in Mars portal successfully
When I delete all records in the education page

Scenario Outline: 02 - Add a new education with valid details
    Given I logged in Mars portal successfully
    When I create a new education with ID <id>
    Then the education with ID <id> should be created successfully

    Examples:
    | id |
    | 1  |

Scenario Outline: 03 - Add a new education with special character
    Given I logged in Mars portal successfully
    When I create a new education with ID <id>
    Then the education with ID <id> should be created successfully

    Examples:
   | id |
   | 2  |

Scenario Outline: 04 - Leave the college textbox empty
    Given I logged in Mars portal successfully
    When I leave the college textbox with ID <id>
    Then the education with ID <id> shouldnot be created successfully

    Examples:
   | id |
   | 3  |

Scenario Outline: 05 - Add a new education with special character
    Given I logged in Mars portal successfully
    When I create a new education with ID <id>
    Then the education with ID <id> should be created successfully

    Examples:
   | id |
   | 4  |

Scenario Outline: 06 - Delete an existing education
    Given I logged in Mars portal successfully
    When I delete an existing education with ID <id>
    Then the education with ID <id> should be deleted successfully

    Examples:
   | id |
   | 1  |

