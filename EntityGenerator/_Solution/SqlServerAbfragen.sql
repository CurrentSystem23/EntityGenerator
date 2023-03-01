--Alle Datenbanken eines Servers ermitteln
SELECT d.[name]        AS [object_name], 
       d.[database_id] AS [object_id],
       'Database'      AS [object_type]
  FROM sys.databases AS d
 WHERE d.[name] = 'DevDays'
;

--Alle Schemas (einer Datenbank) ermitteln
SELECT iss.[schema_name]  AS [object_name],
       ss.[schema_id]     AS [object_id],
       'Schema'           AS [object_type],
	   iss.[catalog_name] AS [database]
  FROM information_schema.schemata AS iss
 INNER JOIN sys.schemas AS ss ON ss.[name] = iss.[schema_name]
 WHERE iss.catalog_name = 'DevDays'
   AND iss.schema_owner = 'dbo' -- ist dbo hier wirklich die richtige Einschränkung?
;

--Alle Objekte (einer Datenbank) ermitteln
SELECT o.[name]           AS [object_name],
       o.[id]             AS [object_id],
       o.xtype            AS [object_type],
       ss.[name]          AS [schema_name],
       iss.[catalog_name] AS [database_name]
  FROM sys.sysobjects AS o
 INNER JOIN sys.schemas ss ON o.uid = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 WHERE iss.catalog_name = 'DevDays'
   AND ss.[name] <> 'sys'
;

--Alle benutzten Typen von Objekten (einer Datenbank) ermitteln
SELECT DISTINCT 
       o.xtype            AS [object_name],
       o.uid              AS [object_id],
       'Type'             AS [object_type],
	     iss.[catalog_name] AS [database]
  FROM sys.sysobjects AS o
 INNER JOIN sys.schemas ss ON o.uid = ss.schema_id
 INNER JOIN information_schema.schemata AS iss ON ss.name = iss.schema_name 
 WHERE iss.catalog_name = 'DevDays'
--   AND o.xtype = 'U'
;

--AF : Aggregate function (CLR)
--C  : CHECK Constraint
--D  : Default or DEFAULT Constraint
--F  : FOREIGN KEY Constraint
--FN : Scalar Function
--FS : Assembly (CLR) Scalar-Function
--FT : Assembly (CLR) Table-Valued Function
--IF : In-lined Table Function
--IT : Internal Table
--L  : Log
--P  : Stored Procedure
--PC : Assembly (CLR) Stored Procedure
--PK : PRIMARY KEY Constraint (Type is K)
--RF : Replication Filter Stored Procedure
--S  : System Table
--SN : Synonym
--SQ : Service Queue
--TA : Assembly (CLR) DML Trigger
--TF : Table Function
--TR : SQL DML Trigger
--TT : Table Type
--U  : User Table
--UQ : UNIQUE Constraint
--V  : View
--X  : Extended Stored Procedure



--Alle Spalteninformationen ermitteln
SELECT c.[name]                      AS [object_name],
       c.[column_id]                 AS [object_id],
       'Column'                      AS [object_type],
       o.[name]                      AS [table_name],
       ss.[name]                     AS [schema_name],
       iss.[catalog_name]            AS [database_name],
       t.[name]                      AS [column_type],
       c.[is_identity]               AS [column_is_identity],
       c.[is_nullable]               AS [column_is_nullable],
       d.[definition]                AS [column_default_definition],
       c.[is_computed]               AS [column_is_computed],
       c.[max_length]                AS [column_max_length],
       isc.[character_octet_length]  AS [column_character_octet_length],
       isc.[numeric_precision]       AS [column_numeric_precision],
       isc.[numeric_precision_radix] AS [column_numeric_precision_radix],
       isc.[numeric_scale]           AS [column_numeric_scale],
       isc.[datetime_precision]      AS [column_datetime_precision]
  FROM sys.columns AS c 
 INNER JOIN sys.types AS t ON c.[user_type_id] = t.[user_type_id]
 INNER JOIN sys.sysobjects AS o ON c.[object_id] = o.[id]
 INNER JOIN sys.schemas ss ON o.uid = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 INNER JOIN information_schema.columns AS isc ON  iss.[catalog_name] = isc.[table_catalog] AND ss.[name] = isc.[table_schema] AND o.[name] = isc.[table_name] AND c.[name] = isc.[column_name]
  LEFT JOIN sys.default_constraints as d ON d.[object_id] = c.[default_object_id]
 WHERE iss.[catalog_name] = 'DevDays'
   AND ss.[name] <> 'sys'
   AND o.[xtype] = 'U'
 ORDER BY iss.[catalog_name], ss.[name], o.[name], c.[column_id];

--Funktionen auslesen
SELECT obj.[name]                                            AS [object_name],
       obj.[object_id]                                       AS [object_id],
       obj.[type]                                            AS [object_type],
	     ss.[name]                                             AS [schema_name],
	     iss.[catalog_name]                                    AS [database_name],
	     CASE obj.[type]
         WHEN 'AF' THEN 'Aggregate function'
         WHEN 'FN' THEN 'SQL scalar function'
         WHEN 'TF' THEN 'SQL inline table-valued function'
         WHEN 'IF' THEN 'SQL table-valued-function'
         WHEN 'FS' THEN 'Assembly (CLR) Scalar-Function'
         WHEN 'FT' THEN 'Assembly (CLR) Table-Valued Function'
         WHEN 'P'  THEN 'Stored Procedure'
       END                                                   AS [object_type_name],
       SUBSTRING(par.[parameters], 0, LEN(par.[parameters])) AS [function_parameters],
       TYPE_NAME(ret.[user_type_id])                         AS [function_return_type],
       mod.[definition]                                      AS [function_definition]
  FROM sys.objects obj
 INNER JOIN sys.sql_modules mod ON mod.[object_id] = obj.[object_id]
 INNER JOIN sys.schemas AS ss ON obj.[schema_id] = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 CROSS APPLY (SELECT p.name + ' ' + TYPE_NAME(p.[user_type_id]) + ', '
                FROM sys.parameters p
               WHERE p.[object_id] = obj.[object_id]
                 AND p.[parameter_id] != 0
                 FOR XML PATH ('') ) AS par (parameters)
  LEFT JOIN sys.parameters ret ON obj.[object_id] = ret.[object_id] AND ret.[parameter_id] = 0
 WHERE obj.[type] in ('AF', 'FN', 'TF', 'IF', 'FS', 'FT', 'P')
 ORDER BY iss.[catalog_name],
          iss.[schema_name],
          obj.[name];

-- Rückgabewerte von Table Value Functions
SELECT c.[name]                      AS [object_name],
       c.[column_id]                 AS [object_id],
       'Column'                      AS [object_type],
       o.[name]                      AS [function_name],
       s.[name]                      AS [schema_name],
       iss.[catalog_name]            AS [database],
       t.[name]                      AS [column_type],
       c.[is_identity]               AS [column_is_identity],
       c.[is_nullable]               AS [column_is_nullable],
       d.[definition]                AS [column_default_definition],
       c.[is_computed]               AS [column_is_computed],
       c.[max_length]                AS [column_max_length],
       t.[precision]                 AS [column_numeric_precision],
       t.[scale]                     AS [column_numeric_scale]
  FROM sys.columns AS c
 INNER JOIN sys.types AS t ON t.system_type_id = c.system_type_id
 INNER JOIN sys.sysobjects o ON o.id = c.object_id
 INNER JOIN sys.schemas s ON o.uid = s.schema_id
 INNER JOIN information_schema.schemata AS iss ON s.[name] = iss.[schema_name]
  LEFT JOIN sys.default_constraints as d ON d.[object_id] = c.[default_object_id]
 WHERE iss.[catalog_name] = 'DevDays'
 ORDER BY iss.[catalog_name],
          s.[name],
          o.name, 
          c.column_id;


--https://www.mssqltips.com/sqlservertip/5384/working-with-sql-server-extended-properties/
-- Abrufen der Extended Properties für einzelne Spalten
-- Nutzen könnte zum Beispiel in der Generierung von Zusatzinformation oder Einschränkung von Wertebreichen sein,
-- aber Achtung diese Einschränkungen sind nicht für die Datenbank relevant!
-- Alle Extended Properties die an Columns hängen
SELECT p.[name]                                              AS [object_name],
       -1                                                    AS [object_id],
       'ColumnExtendedProperty'                              AS [object_type],
       clmns.[name]                                          AS ColumnName,
       tbl.[name]                                            AS TableName, 
       ss.[name]                                             AS [schema_name],
       iss.[catalog_name]                                    AS [database_name],
       CAST(p.[value] AS sql_variant)                        AS ExtendedPropertyValue
  FROM sys.tables AS tbl
 INNER JOIN sys.all_columns AS clmns ON clmns.[object_id] = tbl.[object_id]
 INNER JOIN sys.extended_properties AS p ON p.[major_id] = tbl.[object_id] AND p.[minor_id] = clmns.[column_id] AND p.[class] = 1
 INNER JOIN sys.schemas AS ss ON tbl.[schema_id] = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 WHERE iss.[catalog_name] = 'DevDays'
 ORDER BY iss.[catalog_name],
          iss.[schema_name],
          tbl.[name],
          clmns.[name],
          p.[name];

--Anlegen eines Extended Properties
EXEC sp_addextendedproperty  
     @name = N'SNO' 
    ,@value = N'Testing entry for Extended Property' 
    ,@level0type = N'Schema', @level0name = 'core' 
    ,@level1type = N'Table',  @level1name = 'DomainValue' 
    ,@level2type = N'Column', @level2name = 'ValueC'
GO


-- Abrufen der Extended Properties für Tabellen
-- Die Extended Properties können wir zum Beispiel für die PUBLIC RestAPI Actions nutzen
SELECT p.[name]                                              AS [object_name],
       -1                                                    AS [object_id],
       'TableExtendedProperty'                               AS [object_type],
       tbl.[name]                                            AS TableName, 
       ss.[name]                                             AS [schema_name],
       iss.[catalog_name]                                    AS [database_name],
       CAST(p.[value] AS sql_variant)                        AS ExtendedPropertyValue
  FROM sys.tables AS tbl
 INNER JOIN sys.extended_properties AS p ON p.[major_id] = tbl.[object_id] AND p.[class] = 1 AND p.[minor_id] = 0
 INNER JOIN sys.schemas AS ss ON tbl.[schema_id] = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 WHERE iss.[catalog_name] = 'DevDays'
 ORDER BY iss.[catalog_name],
          iss.[schema_name],
          tbl.[name],
          p.[name];

-- List all user defined table types
SELECT tt.[name]                     AS [object_name],
       tt.[type_table_object_id]     AS [object_id],
       'UserDefinedTableType'        AS [object_type],
       o.[name]                      AS [table_name],
       ss.[name]                     AS [schema_name],
       iss.[catalog_name]            AS [database_name]
  FROM sys.table_types AS tt
 INNER JOIN sys.sysobjects AS o ON tt.[type_table_object_id] = o.[id]
 INNER JOIN sys.schemas ss ON tt.[schema_id] = ss.[schema_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 WHERE tt.[is_user_defined] = 1;

-- List all user defined table types with Columns
SELECT tt.[name]                     AS [TableTypeName],
       tt.[type_table_object_id]     AS [object_id],
       'UserDefinedTableTypeColumn'  AS [object_type],
       o.[name]                      AS [table_name],
       ss.[name]                     AS [schema_name],
       iss.[catalog_name]            AS [database_name],
       c.[name]                      AS [Field],
       t.[name]                      AS [Type],
       c.[is_identity]               AS [column_is_identity],
       c.[is_nullable]               AS [column_is_nullable],
       d.[definition]                AS [column_default_definition],
       c.[is_computed]               AS [column_is_computed],
       c.[max_length]                AS [column_max_length]
  FROM sys.table_types AS tt
 INNER JOIN sys.sysobjects AS o ON tt.[type_table_object_id] = o.[id]
 INNER JOIN sys.schemas ss ON tt.[schema_id] = ss.[schema_id]
 INNER JOIN sys.columns AS c on c.[object_id] = tt.[type_table_object_id]
 INNER JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
 INNER JOIN sys.types AS t ON c.[system_type_id] = t.[system_type_id]
  LEFT JOIN sys.default_constraints as d ON d.[object_id] = c.[default_object_id]
 WHERE iss.[catalog_name] = 'Lenny'
   AND t.[name] != 'sysname'
 ORDER BY iss.[catalog_name],
          iss.[schema_name],
          tt.[name],
          c.column_id

-- List all foreign keys
SELECT obj.[name]         AS [object_name],
       obj.[object_id]    AS [object_id],
       'ForeignKey'       AS [object_type],
       iss.[catalog_name] AS [database_name],
       sch1.[name]        AS [schema_name],
       tab1.[name]        AS [table],
       col1.[name]        AS [column],
       sch2.[name]        AS [referenced_schema_name],
       tab2.[name]        AS [referenced_table],
       col2.[name]        AS [referenced_column]
  FROM sys.foreign_key_columns fkc
 INNER JOIN sys.objects obj ON obj.object_id = fkc.constraint_object_id
 INNER JOIN sys.tables tab1 ON tab1.object_id = fkc.parent_object_id
 INNER JOIN sys.schemas sch1 ON tab1.schema_id = sch1.schema_id
 INNER JOIN sys.columns col1 ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id
 INNER JOIN sys.tables tab2 ON tab2.object_id = fkc.referenced_object_id
 INNER JOIN sys.columns col2 ON col2.column_id = referenced_column_id AND col2.object_id = tab2.object_id
 INNER JOIN sys.schemas sch2 ON tab2.schema_id = sch2.schema_id
 INNER JOIN information_schema.schemata AS iss ON sch1.[name] = iss.[schema_name]
 WHERE iss.[catalog_name] = 'Lenny'
 ORDER BY iss.[catalog_name],
          sch1.[name],
          tab1.[name],
          col1.[name]

-- List all constraints
SELECT a.[constraint_name] AS [object_name],
       a.[object_id]       AS [object_id],
       'Constraint'        AS [object_type],
       iss.[catalog_name]  AS [database_name],
       a.[Schema]          AS [schema_name],
       a.[table_view]      AS [table_view_name],
       a.[object_type]     AS [source_type], 
       a.[constraint_type] AS [constraint_type],
       a.[details]         AS [Columns],
       a.[TargetSchema]    AS [TargetSchema],
       a.[TargetTable]     AS [TargetTable],
       a.[definition]      AS [definition]
  FROM (
    --Primary key and Unique constraint
    SELECT SCHEMA_NAME(t.[schema_id]) AS [Schema],
           t.[name] AS table_view, 
           CASE WHEN t.[type] = 'U'  THEN 'Table'
                WHEN t.[type] = 'V'  THEN 'View'
                END AS [object_type],
           CASE WHEN c.[type] = 'PK' THEN 'Primary key'
                WHEN c.[type] = 'UQ' THEN 'Unique constraint'
                WHEN i.[type] = 1    THEN 'Unique clustered index'
                WHEN i.[type] = 2    THEN 'Unique index'
               END AS constraint_type, 
           ISNULL(c.[name], i.[name]) AS constraint_name,
           SUBSTRING(column_names, 1, LEN(column_names)-1) AS [details],
           t.[object_id] AS [object_id],
           '' AS [definition],
           '' AS [TargetSchema],
           '' AS [TargetTable]
      FROM sys.objects t
      LEFT OUTER JOIN sys.indexes i ON t.[object_id] = i.[object_id]
      LEFT OUTER JOIN sys.key_constraints c ON i.[object_id] = c.[parent_object_id] AND i.[index_id] = c.[unique_index_id]
     CROSS APPLY (SELECT col.[name] + ', '
                    FROM sys.index_columns ic
                   INNER JOIN sys.columns col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
                   WHERE ic.[object_id] = t.[object_id]
                     AND ic.[index_id] = i.[index_id]
                   ORDER BY col.[column_id]
                    FOR XML PATH ('') 
                  ) AS D (column_names)
     WHERE [is_unique] = 1
       AND t.[is_ms_shipped] <> 1

     UNION ALL 

    --Foreign key constraints
    SELECT SCHEMA_NAME(fk_tab.[schema_id]) AS [Schema],
           fk_tab.[name] AS foreign_table,
           'Table',
           'Foreign key',
           fk.[name] AS fk_constraint_name,
           SCHEMA_NAME(pk_tab.[schema_id]) + '.' + pk_tab.[name],
           fk.[object_id] AS [object_id],
           '' AS [definition],
           SCHEMA_NAME(pk_tab.[schema_id]) AS [TargetSchema],
           pk_tab.[name] AS [TargetTable]
      FROM sys.foreign_keys fk
     INNER JOIN sys.tables fk_tab ON fk_tab.[object_id] = fk.[parent_object_id]
     INNER JOIN sys.tables pk_tab ON pk_tab.[object_id] = fk.referenced_object_id
     INNER JOIN sys.foreign_key_columns fk_cols ON fk_cols.[constraint_object_id] = fk.[object_id]

     UNION ALL 

    --Check constraint
    SELECT SCHEMA_NAME(t.[schema_id]) AS [Schema],
           t.[name],
           'Table',
           'Check constraint',
           con.[name] AS constraint_name,
           '',
           t.[object_id] AS [object_id],
           con.[definition] AS [definition],
           '' AS [TargetSchema],
           '' AS [TargetTable]
      FROM sys.check_constraints con
      LEFT OUTER JOIN sys.objects t ON con.[parent_object_id] = t.[object_id]
      LEFT OUTER JOIN sys.all_columns col ON con.[parent_column_id] = col.[column_id] AND con.[parent_object_id] = col.[object_id]

     UNION ALL 

    --Default constraint
    SELECT SCHEMA_NAME(t.[schema_id]) AS [Schema],
           t.[name],
           'Table',
           'Default constraint',
           con.[name],
           col.[name],
           t.[object_id] AS [object_id],
           col.[name] + ' = ' + con.[definition] AS [definition],
           SCHEMA_NAME(t.[schema_id]) AS [TargetSchema],
           t.[name] AS [TargetTable]
      FROM sys.default_constraints con
      LEFT OUTER JOIN sys.objects t ON con.[parent_object_id] = t.[object_id]
      LEFT OUTER JOIN sys.all_columns col ON con.[parent_column_id] = col.[column_id] AND con.[parent_object_id] = col.[object_id]

       ) AS a
 INNER JOIN information_schema.schemata AS iss ON a.[Schema] = iss.[schema_name]
 WHERE iss.[catalog_name] = 'Lenny'
 ORDER BY iss.[catalog_name],
          a.[Schema],
          a.[table_view], 
          a.[constraint_type], 
          a.[constraint_name]

-- List all indexes
SELECT i.[name]                                                          AS [object_name],
       i.[object_id]                                                     AS [object_id],
       'Index'                                                           AS [object_type],
       t.[name]                                                          AS [table_name],
       SCHEMA_NAME(t.schema_id)                                          AS [schema_name],
       iss.[catalog_name]                                                AS [database_name],
       SUBSTRING(column_names, 1, LEN(column_names)-1)                   AS [index_columns],
       SUBSTRING(included_column_names, 1, LEN(included_column_names)-1) AS [included_columns],
       i.[type]                                                          AS [index_type_id],
       CASE 
         WHEN i.[type] = 1 THEN 'Clustered index'
         WHEN i.[type] = 2 THEN 'Nonclustered unique index'
         WHEN i.[type] = 3 THEN 'XML index'
         WHEN i.[type] = 4 THEN 'Spatial index'
         WHEN i.[type] = 5 THEN 'Clustered columnstore index'
         WHEN i.[type] = 6 THEN 'Nonclustered columnstore index'
         WHEN i.[type] = 7 THEN 'Nonclustered hash index'
       END                                             AS [index_type],
       i.[is_unique]                                   AS [is_unique],
       CASE 
         WHEN i.[is_unique] = 1 THEN 'Unique'
         ELSE 'Not unique' 
       END                                             AS [unique],
       t.[type]                                        AS [table_type],
       CASE 
         WHEN t.[type] = 'U' THEN 'Table'
         WHEN t.[type] = 'V' THEN 'View'
       END                                             AS [object_type]
  FROM sys.objects t
 INNER JOIN sys.indexes i ON t.[object_id] = i.[object_id]
 CROSS APPLY (SELECT col.[name] + ', '
                FROM sys.index_columns ic
               INNER JOIN sys.columns col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
               WHERE ic.[object_id] = t.[object_id]
                 AND ic.[index_id] = i.[index_id]
                 AND ic.[is_included_column] = 0
               ORDER BY key_ordinal
                 FOR XML PATH ('') ) AS D (column_names)
 CROSS APPLY (SELECT col.[name] + ', '
                FROM sys.index_columns ic
               INNER JOIN sys.columns col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
               WHERE ic.[object_id] = t.[object_id]
                 AND ic.[index_id] = i.[index_id]
                 AND ic.[is_included_column] = 1
               ORDER BY key_ordinal
                 FOR XML PATH ('') ) AS Dincluded (included_column_names)
 INNER JOIN information_schema.schemata AS iss ON SCHEMA_NAME(t.[schema_id]) = iss.[schema_name]
 WHERE iss.[catalog_name] = 'Lenny'
   AND t.[is_ms_shipped] <> 1
   AND [index_id] > 0
 ORDER BY i.[name]

-- List all triggers
SELECT DISTINCT 
       tr.[name]                   AS [object_name],
       tr.[object_id]              AS [object_id],
       'Trigger'                   AS [object_type],
       o.[name]                    AS [table_name],
       ss.[name]                   AS [schema_name],
       iss.[catalog_name]          AS [database_name],
       tr.[parent_class]           AS [parent_class],
       tr.[parent_class_desc]      AS [parent_class_desc],
       tr.[type]                   AS [trigger_type],
       tr.[type_desc]              AS [trigger_type_desc],
       tr.[is_ms_shipped]          AS [is_ms_shipped],
       tr.[is_disabled]            AS [is_disabled],
       tr.[is_not_for_replication] AS [is_not_for_replication],
       tr.[is_instead_of_trigger]  AS [is_instead_of_trigger],
       mod.[definition]            AS [definition]
  FROM sys.Triggers AS tr
 INNER JOIN sys.sql_modules AS mod ON mod.[object_id] = tr.[object_id]
  LEFT JOIN sys.sysobjects AS o ON tr.[parent_id] = o.[id]
  LEFT JOIN sys.schemas AS ss ON o.[uid] = ss.[schema_id]
  LEFT JOIN information_schema.schemata AS iss ON ss.[name] = iss.[schema_name]
  WHERE iss.[catalog_name] IS NULL OR iss.[catalog_name] = 'Lenny'
    AND tr.[type] IN ('TR', 'TA')
ORDER BY iss.[catalog_name], ss.[name], tr.[name]
