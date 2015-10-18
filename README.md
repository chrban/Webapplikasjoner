#Kaffeplaneten
##Prosjektstruktur
###Database:
CustomerContext.cs innholder Code First filen til databasen. Filen ligger i Models mappen.
Databasen heter DatabaseKaffeplaeneten.
####Database lag:
DBCustomer.cs, DBUser.cs, DBOrder.cs og DBProduct.cs tar seg av all aksess til databasen. 
###MVC lag
####Model:
Alle modelklasser ligger i Models mappen. Disse representerer de ulike databaseentitetene.
####Controller:
Alle kontrollerklassene ligger i Controllers mappen. Diss styrer dataflyten mellom databasen og viewene.
####View:
De ulike viewene er organisert i undermapper i Views mappen. 
###Annet
####DataCreator:
DataCreator har metode for å generere data til databasen. Brukes til å fylle databasen med produkter. Classen har også metoder for å generere testdata.
####SuperController:
Vi valgte å ha en egen SuperController alle kontrollerene arver fra. 
Dette gjør det mulig å ha felles metoder alle kontrollerene har tilgang til. Vi valgte også å opprette en const string for hver Session variabel vi bruker. 
Dette sikrer mot skrivefeil i Session stringen. Disse ligger også i SuperController.cs.
####Kodespråk:
Vi har valgt å kun bruke engelsk i koden. Dette er god kodepraksis da man ikke skal behøve å være norsk for å forstå koden. Kommentarer er på norsk. 
##Beskrivelse av prosjektet:
Prosjektet er en nettside med nettbutikk funksjonalitet. Nettsiden har funksjonalitet for brukerregistrering, innlogging,  oppdatering av brukerdata, visning og kjøp av produkter, og visning av tidligere kjøp. Prosjektet er laget med Visual Studio 2015 i MVC .NET.
##Laget av:
Magnus Johan Knalstad, s198760
Christer Bang, s198737
Sondre Husevold. s198755
Magnus Tønsager, s198761
