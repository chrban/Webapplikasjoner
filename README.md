#Kaffeplaneten

##Prosjektstruktur
###Database
CustomerContext.cs innholder Code First filen til databasen. Filen ligger i Models mappen.
Databasen heter DatabaseKaffeplaeneten.
###Database lag:
DBCustomer.cs, DBUser.cs, DBOrder.cs og DBProduct.cs tar seg av all aksess til databasen. 
###MVC lag:
###Model:
Alle modelklasser ligger i Models mappen. Disse representerer de ulike databaseentitetene.
###Controller:
Alle kontrollerklassene ligger i Controllers mappen. Diss styrer dataflyten mellom databasen og viewene.
###View
De ulike viewene er organisert i undermapper i Views mappen. 

##Beskrivelse av prosjektet
Prosjektet er en nettside med nettbutikk funksjonalitet. Nettsiden har funksjonalitet for brukerregistrering, innlogging,  oppdatering av brukerdata, visning og kjøp av produkter, og visning av tidligere kjøp. Prosjektet er laget med Visual Studio 2015 i MVC .NET.
##Laget av
*Magnus Johan Knalstad, s
*Christer Bang, s
*Sondre Husevold. s
*Magnus Tønsager, s19
