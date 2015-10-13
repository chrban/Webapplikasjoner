# Webapplikasjoner

*Database:*
-	Brukerdatabase
-	Varedatabase
-	Database klasser
-	EF diagram

*HTML:*
-	Web side, Design
-	CSS/Bootsrap

Kode:
-	Innloggingsystem
-	Kode for behandling av brukerdata
-	Validering

Struktur
-	Klassediagram
-	Sekvensdiagram


Database
=========
_Generere databasen._ 

*_Controllers:_*

UserController -  lage nye brukere, endre brukere, slette brukere. 
LoginController - Innlogging. 
ProductController - kontroller for generering av produktlister. Hente ut produktdetaljer. 
SecurityController - validering, passordkryptering. 
OrderController - Handlevogn, registrering av ordre. 

*_Views:_*

HomeView/index
<Et view for hver kaffekategori>ikke nødvendig
ProductDetailsView
LoginView
NewUserView
ShoppingCartView
OrderView(bekrefte pris, adresse og kortinformasjon)
ReceiptView
AccountView(navn, adresse, telefon
OrderHistoryView


*Knalstad:*
-	UserController+LoginController:
-	LoginView
-	NewUserView

*Christer:*
-	ProductController
-	HomeView/index
-	<Et view for hver kaffekategori>ikke nødvendig
-	ProductDetailsView

*Sondre:*
-	OrderController
-	ShoppingCartView
-	OrderView(bekrefte pris, adresse og kortinformasjon)
-	ReceiptView

*Tønsager:*
-	SecurityController
-	AccountView(navn, adresse, telefon)
-	OrderHistoryView
