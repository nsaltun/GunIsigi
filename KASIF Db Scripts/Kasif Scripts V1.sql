SCRIPTS : 

********************--OK
ALTER TABLE dbo.MENU_TREE
ADD  NODE_VISIBILITY [nvarchar](50) null
GO
;--OK

-- INSERT SCRIPTS(ÇALIŞTIRILDI)
	SELECT * FROM MENU_TREE WHERE GUID=15 AND STATUS=1;--OK
	SELECT * FROM QUERY_TABLE WHERE QUERY_NAME = 'GET_ALLOWED_PAGES' AND STATUS=1;--OK
	SELECT * FROM USER_ROLE_MENU WHERE ROLE_GUID = 5 AND NODE_GUID = 6;--OK

	
-- UPDATE SCRIPTS(ÇALIŞTIRILDI)
	UPDATE MENU_TREE
	SET NODE_VISIBILITY='EVERYONE'
	WHERE GUID IN (1,3,14,15);

	UPDATE MENU_TREE
	SET NODE_VISIBILITY='ADMIN'
	WHERE GUID IN (2,4);

	UPDATE MENU_TREE
	SET NODE_VISIBILITY='ROLE'
	WHERE GUID NOT IN (1,2,3,4,14,15);
	
	------------------------------
	UPDATE QUERY_TABLE WHERE QUERY_NAME = "GET_DEVAMSIZLIK_BILGI";--OK
	UPDATE QUERY_TABLE WHERE QUERY_NAME = "GET_OGR_DEVAMSIZLIK_BILGI";--OK
	...--HEPSI DEV DB'SINDEN ALINIP AKTARILDI.
	
	
-- DELETE SCRIPTS(ÇALIŞTIRILDI)
	DELETE FROM USER_ROLE_MENU 
	WHERE status=1 and ROLE_GUID=5 and NODE_GUID=8;
	DELETE FROM USER_ROLE_MENU 
	WHERE status=1 and ROLE_GUID=3 and NODE_GUID=9;
	
-- TABLE DESIGN (ÇALIŞTIRILDI)
	DEVAMSIZLIK_BILGI -> Column Name : HAFTA_ID => DATE   Column Type : int => VARCHAR(50);
	....

********************