# SQL Command Builder

![image](https://user-images.githubusercontent.com/65581934/178659830-9d8eae4a-9125-4344-95bb-f74b11836086.png)

The excel file will be auto-created when running this program. 

![image](https://user-images.githubusercontent.com/65581934/178646892-d9c1c791-94b0-4e8c-b27d-89c36e7d5635.png)

### Tags
Use in excel column "修改註記V"
| Tag  |    Description     |
| :--- | :----------------: |
| AT   |    Create Table    |
| A    |     Add Column     |
| D    |   Delete Column    |
| C    | Change Column Name |
| M    | Modify Column Type |


### Functions
#### 1. Create Table
* Collect "AT" tag rows and build create table command string.
#### 2. Alter Column
* Collect "A", "D", "C", "M" tag rows and build alter column command string and stored procedure.
#### 3. Create and Alter
* Collect all tag rows and build command string.
* Actually the function is 1 + 2.
#### 4. Copy Content
* Copy the content of the text area.
