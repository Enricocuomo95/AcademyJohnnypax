Quest'applicativo è stato molto importante per comprendere come connettere il db con il mio lato applicativo (c#).
Per far ciò ho utilizzato una stringa di connessione, anzi abbiamo settato in un file json due 
tringhe di connessione uno per la connessione locale, e l'altra per il remoto. Abbiamo dovuto scaricare i driver tramite 
Nuget con il System.Data.SQLClient. Abbiamo infatti creato un applicativo client-server. Configuarto con Extensions.Configurate.Json:

This package enables you to read your application's settings from a JSON file. You can use JsonConfigurationExtensions.
AddJsonFile extension method on IConfigurationBuilder to add the JSON configuration provider to the configuration builder.
e estension.Configuration: Implementation of key-value pair based configuration for Microsoft.Extensions.
Configuration. Includes the memory configuration provider.

Abbiamo poi creat un Model e un DAL (Data Access Layer) al suo interno abbiamo messo le operazioni di business e abbiao implementato 
un interfaccia solo ed escusivamente per le CRUD per avere una facade verso l'esterno.
Il proggetto ha subito migliorie con l'ausilio di lsql che mi hanno permesso di migliorare la velocità dell'applicativo facendo query su liste

