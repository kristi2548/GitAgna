DECLARE @id INT
declare @value as varchar(3)
SET @id = 1
WHILE (@id <= 999)
BEGIN
	IF (LEN(CAST(@ID AS VARCHar(3))) = 1 ) begin set @value = '00' + CAST(@ID AS VARCHar(1)) end 
	IF (LEN(CAST(@ID AS VARCHar(3))) = 2 ) begin set @value = '0' + CAST(@ID AS VARCHar(2)) end 
	IF (LEN(CAST(@ID AS VARCHar(3))) = 3 ) begin set @value =  CAST(@ID AS VARCHar(3)) end 
	INSERT INTO [dbo].[wCellValues] ([CellValue]) VALUES (@value)
	print @value
    SELECT @id = @id + 1
END