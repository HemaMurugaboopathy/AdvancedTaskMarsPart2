Feature: CertificationFeature

As a user, 
I want to login and create and delete my certification so that I can manage my certification list for skill trade.

@tag1
Scenario Outline: 01 - Delete all records before adding any certification
Given User logged into Mars URL and navigates to Certifications tab
When I delete all records in the certification page

Scenario Outline: 02 - Add a new certification with valid details
    Given User logged into Mars URL and navigates to Certifications tab
    When I create a new certification with ID <id>
    Then the certification with ID <id> should be created successfully

    Examples:
    | id |
    | 1  |

Scenario Outline: 03 - Add a new certification with special character
    Given User logged into Mars URL and navigates to Certifications tab
    When I create a new certification with ID <id>
    Then the certification with ID <id> should be created successfully

    Examples:
   | id |
   | 2  |

Scenario Outline: 04 - Leave the certificate textbox empty
    Given User logged into Mars URL and navigates to Certifications tab
    When I leave the certificate textbox with ID <id>
    Then the certification with ID <id> shouldnot be created successfully

    Examples:
   | id |
   | 3  |

Scenario Outline: 05 - Add a new certification with special character
    Given User logged into Mars URL and navigates to Certifications tab
    When I create a new certification with ID <id>
    Then the certification with ID <id> should be created successfully

    Examples:
   | id |
   | 4  |

Scenario Outline: 06 - Delete an existing certification
    Given User logged into Mars URL and navigates to Certifications tab
    When I delete an existing certification with ID <id>
    Then the certification with ID <id> should be deleted successfully

    Examples:
   | id |
   | 1  |
