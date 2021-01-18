CREATE FUNCTION getConsumptionQuartiles(siteId integer)
RETURNS 
    TABLE (
        ResidentialQ1 double precision,
        ResidentialQ3 double precision,
        CommercialQ1 double precision,
        CommercialQ3 double precision,
        ProductiveQ1 double precision,
        ProductiveQ3 double precision,
        PublicQ1 double precision,
        PublicQ3 double precision,
        PeakLoadQ1 double precision,
        PeakLoadQ3 double precision,
        TotalQ1 double precision,
        TotalQ3 double precision
    )  
AS $$
BEGIN
    RETURN QUERY 
        WITH CTE AS
        (
	        SELECT 
    	        *
            FROM public."Consumptions"
	        WHERE "SiteId" = siteId
        )
        SELECT 
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "Residential")
            FROM CTE
            WHERE "Residential" IS NOT NULL 
        ) AS ResidentialQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "Residential")
            FROM CTE
            WHERE "Residential" IS NOT NULL 
        ) AS ResidentialQ3,
  
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "Commercial")
            FROM CTE
            WHERE "Commercial" IS NOT NULL 
        ) AS CommercialQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "Commercial")
            FROM CTE
            WHERE "Commercial" IS NOT NULL 
        ) AS CommercialQ3,
  
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "Productive")
            FROM CTE
            WHERE "Productive" IS NOT NULL 
        ) AS ProductiveQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "Productive")
            FROM CTE
            WHERE "Productive" IS NOT NULL 
        ) AS ProductiveQ3,
  
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "Public")
            FROM CTE
            WHERE "Public" IS NOT NULL 
        ) AS PublicQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "Public")
            FROM CTE
            WHERE "Public" IS NOT NULL 
        ) AS PublicQ3,
  
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "PeakLoad")
            FROM CTE
            WHERE "PeakLoad" IS NOT NULL 
        ) AS PeakLoadQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "PeakLoad")
            FROM CTE
            WHERE "PeakLoad" IS NOT NULL 
        ) AS PeakLoadQ3,
  
        (
            SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "Total")
            FROM CTE
            WHERE "Total" IS NOT NULL 
        ) AS TotalQ1,
        (
            SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "Total")
            FROM CTE
            WHERE "Total" IS NOT NULL 
        ) AS TotalQ3;
END;
$$ LANGUAGE plpgsql;