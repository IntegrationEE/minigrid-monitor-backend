CREATE FUNCTION getFinanceOpexQuartiles(siteId integer)
RETURNS 
    TABLE (
        SiteSpecificQ1 double precision,
        SiteSpecificQ3 double precision,
        CompanyLevelQ1 double precision,
        CompanyLevelQ3 double precision,
        TaxesQ1 double precision,
        TaxesQ3 double precision,
        LoanRepaymentsQ1 double precision,
        LoanRepaymentsQ3 double precision
    )  
AS $$
BEGIN
    RETURN QUERY 
        WITH CTE AS
        (
	        SELECT 
    	        *
            FROM public."FinanceOpex"
	        WHERE "SiteId" = siteId
        )
        SELECT 
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "SiteSpecific")
            FROM CTE
            WHERE "SiteSpecific" IS NOT NULL 
        ) AS SiteSpecificQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "SiteSpecific")
            FROM CTE
            WHERE "SiteSpecific" IS NOT NULL 
        ) AS SiteSpecificQ3,
  
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "CompanyLevel")
            FROM CTE
            WHERE "CompanyLevel" IS NOT NULL 
        ) AS CompanyLevelQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "CompanyLevel")
            FROM CTE
            WHERE "CompanyLevel" IS NOT NULL 
        ) AS CompanyLevelQ3,
  
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "Taxes")
            FROM CTE
            WHERE "Taxes" IS NOT NULL 
        ) AS TaxesQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "Taxes")
            FROM CTE
            WHERE "Taxes" IS NOT NULL 
        ) AS TaxesQ3,
  
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "LoanRepayments")
            FROM CTE
            WHERE "LoanRepayments" IS NOT NULL 
        ) AS LoanRepaymentsQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "LoanRepayments")
            FROM CTE
            WHERE "LoanRepayments" IS NOT NULL 
        ) AS LoanRepaymentsQ3;
END;
$$ LANGUAGE plpgsql;