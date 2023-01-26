USE [SalesDB]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetNumOfSalesAndOrdersData]    Script Date: 26/01/2023 21:38:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ==========================================================================================
-- Author:		Orit
-- Create date: 2021-01-18
-- Description:	get data of num of sales in each city from the Top num of rows
-- ==========================================================================================
CREATE PROCEDURE [dbo].[SP_GetNumOfSalesAndOrdersData]
	
	@sortBy varchar(20)= 'MaxNum',
	@sortDirection varchar(4) = 'desc',
	@topNum int =10
	
AS
BEGIN

CREATE TABLE #tmpTable  
(
	ProductID	bigint,  
	ProductName nvarchar(50),
	City		nvarchar(50),
	NumOfSales	int
); 

 insert into #tmpTable 
SELECT TOP (@topNum) oitems.product_id ProductID,product_name ProductName,City,count(*) NumOfSales
  FROM [SalesDB].[sales].[order_items] oitems
  join [production].[products] on oitems.product_id=products.product_id
  join [SalesDB].[sales].[orders] on oitems.order_id= orders.order_id
  left join [SalesDB].[sales].[customers] on orders.customer_id=customers.customer_id
  group by oitems.product_id,product_name,city 
  order by NumOfSales DESC;

  select * from #tmpTable
   ORDER BY 
             CASE when @sortBy='ProductName' and @sortDirection='asc' then ProductName end ASC,
			 CASE when @sortBy='ProductName' and @sortDirection='desc' then ProductName  end DESC,
			 CASE when @sortBy='City' and @sortDirection='asc' then City end ASC,
			 CASE when @sortBy='City' and @sortDirection='desc' then City  end DESC,
			 CASE when @sortBy='NumOfSales' and @sortDirection='asc' then NumOfSales end ASC,
			 CASE when @sortBy='NumOfSales' and @sortDirection='desc' then NumOfSales  end DESC
			 ;

  Drop table #tmpTable;
 
END
GO


