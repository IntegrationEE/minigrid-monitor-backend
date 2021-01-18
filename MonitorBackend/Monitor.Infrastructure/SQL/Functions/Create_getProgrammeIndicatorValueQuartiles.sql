CREATE FUNCTION getProgrammeIndicatorValueQuartiles(indicatorId integer)
RETURNS 
    TABLE (
        ValueQ1 double precision,
        ValueQ3 double precision
    )  
AS $$
BEGIN
    RETURN QUERY 
        WITH CTE AS
        (
	        SELECT 
    	        *
            FROM public."ProgrammeIndicatorValues"
	        WHERE "ProgrammeIndicatorId" = indicatorId
        )
        SELECT 
            (
                SELECT percentile_cont(0.25) WITHIN GROUP (ORDER BY "Value")
                FROM CTE
                WHERE "Value" IS NOT NULL 
            ) AS ValueQ1,
            (
                SELECT percentile_cont(0.75) WITHIN GROUP (ORDER BY "Value")
                FROM CTE
                WHERE "Value" IS NOT NULL 
            ) AS ValueQ3;
END;
$$ LANGUAGE plpgsql;