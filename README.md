# SQL Command Builder

![image](https://user-images.githubusercontent.com/65581934/178645452-3330e821-6708-4339-995f-ef34e574923f.png)

When turn on this program,
excel file will auto created. 

### Adjustment Tag

|   Adjusted Content   | Tag |
| :---------- | :--: |
|   Create Table   |  AT  |
|   Add Column   |  A   |
|   Delete Column   |  D   |
| Change Column Name |  C   |
| Modify Column Type |  M   |

### Function

1. Create Table
  * Program will collect all "AT" tag row, and build create table command string.
2. Alter Column
  * Program will collect all "A", "D", "C", "M" tag row, and build alter column command string and stored procedure.
3. Create and Alter
  * Program will collect all tag row, and build command string.
  * Actually the function is 1 + 2.
4. Copy Content
  * Copy text area's content.
