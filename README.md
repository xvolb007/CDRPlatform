# CDRPlatformCDR Platform API
Overview
The CDR (Call Detail Record) Platform is a robust API for managing and analyzing call detail records. It provides endpoints for importing, querying, and analyzing telecommunication call data.
Features

Import CDR data from CSV files
View call statistics (average cost, total counts, duration)
Filter calls by date ranges
Retrieve calls by caller ID
Paginated access to call records

Dependency Configuration Service
One potential improvement would be to add a dedicated service for managing dependency injection configuration. This would centralize the registration of services and repositories, making the codebase more maintainable.