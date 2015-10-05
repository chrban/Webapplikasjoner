# Webapplikasjoner

Database:
-	brukerdatabase
-	varedatabase
-	database klasser
-	EF diagram

HTML:
-	web side, design
-	css/bootsrap

Kode:
-	innloggingsystem
-	kode for behandling av brukerdata
-	validering

Struktur
-	classediagram
-	sekvensdiagram


Database
Generere databasen. 

Controllers
UserController -  lage nye brukere, endre brukere, slette brukere
LoginController - Innlogging
ProductController - kontroller for generering av produktlister. Hente ut produktdetaljer. 
SecurityController - validering, passordkryptering
OrderController - Handlevogn, registrering av ordre.

Views:
HomeView/index
<Et view for hver kaffekategori>ikke nødvendig
ProductDetailsView
LoginView
NewUserView
ChoppingCartView
OrderView(bekrefte pris, adresse og kortinformasjon)
ReceipView
AcountView(navn, adresse, telefon
OrderHistoryView



**Generere databasen**

Knalstad:
UserController+LoginController:
LoginView
NewUserView

Christer:
ProductController
HomeView/index
<Et view for hver kaffekategori>ikke nødvendig
ProductDetailsView

Sondre:
OrderController
ShoppingCartView
OrderView(bekrefte pris, adresse og kortinformasjon)
ReceipView

Tønsager:
SecurityController
AccountView(navn, adresse, telefon)
OrderHistoryView
