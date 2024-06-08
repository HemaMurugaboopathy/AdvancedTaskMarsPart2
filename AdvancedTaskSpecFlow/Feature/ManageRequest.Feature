Feature: ManageRequest

As s user.
I would like to recieve, sent and complete requests in the manage request

Scenario Outline: 01- Accept the recieved requests in the manage requests
Given User logged into Mars URL and navigates to Manage Request tab
When I click recieved requests and I accepts the received requests with ID '<id>'
Then the manage request with ID <id> should be accepted successfully

 Examples:
    | id |
    | 1  |

Scenario Outline: 02 - Decline the recieved requests in the manage requests
Given User logged into Mars URL and navigates to Manage Request tab
When I click recieved requests and I declines the received requestswith ID '<id>'
Then the manage request with ID <id> should be declined successfully

 Examples:
    | id |
    | 2  |

Scenario Outline: 03 - Complete the received requests in the manage requests
Given User logged into Mars URL and navigates to Manage Request tab
When I click recieved requests and I completes the received requestswith ID '<id>'
Then the received request with ID <id> should be completed successfully

 Examples:
    | id |
    | 3 |

Scenario Outline: 04 - Withdraw the sent request in the manage requests
Given User logged into Mars URL and navigates to Manage Request tab
When I click sent requests 
Then I withdraw the sent requestswith ID '<id>'
Then the withdraw request with ID <id> should be withdrawn successfully

 Examples:
    | id |
    | 1 |


