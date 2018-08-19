DECLARE @id int, @name varchar(50), @date datetime, @filename NVARCHAR(50), @author_Id int;
SET @id=0 
SET @name='PIC';
SET @date = 10-10-2010;
SET @filename ='KEKUS';
SET @author_Id = 0;
EXEC AddDoc @name, @date, @filename, @author_Id