# NorbitsChallenge

Det fyrste eg gjorde når eg skulle begynna på oppgåva, var å setta opp korleis eg skulle gjennomføra den. Det som eg såg måtte bli gjort og funksjonane som skulle implementerast. Når eg no hadde sett opp dette, hadde eg kontroll over kva som var blitt gjort og kva som skulle gjerast. Dette gav meg og muligheiten til å legge til nye punkter og ting som eg kom på undervegs, slik at eg hadde kontroll på alt.

Det fyrste eg implementerte var å få lagt til bilar, slik at eg kunne begynna å fylla opp databasen. Fekk litt feilmeldingar til å begynna med, pga. databasen, noko eg løyste med å laga ein ny database sjølv. 

Eg såg det at “companyId” var ein del av søket når ein skulle søka opp registreringsnummer i databasen. Noko eg skjønte var fordi desse ulike verkstadane har så klart sin eigen ID som kun er deira, men alle bruker same database. Fordi dei då har kvar sin unike ID kan ein då skilja dei ulike verkstadane med at ein då berre registrerer alt frå verkstaden saman med ID-en til verkstaden. Så dette brukte eg i alt når det gjaldt å hente ut liste av alle bilane og når ein skulle legga til ny bil, redigera bil osv. Sidan “companyId” var fast la eg berre den til i søket som “hidden”. 

Eg skjønner sjølvsagt at dette er ein risikabel måte å gjera det på, mtp at ein ganske lett kan manipulera “value” til noko anna, og dermed få tilgang til andre verkstadar sin data. Så dette er absolutt noko ein må løysa på ein annan meir sikker måte. Men i denne oppgåva brukte eg den “usikrare” måten.

Ein annan ting eg nett implementerte i begynnelsen, var det at eg såg at “companyname” var blankt og at eg heller ikkje fekk muligheiten til å endra namnet i innstillingar, fordi Settings databasen var tom. Så her sette eg berre opp ein sjekk i SettingsDb der det automatisk sjekka om “companyname” var tomt, og visst det var så oppdaterte den databasen med eit “default” navn for selskapet. Med dette kunne ein då etterpå også inn i innstillingar og endra namnet til det ein ville.

Lista med bilane i databasen la eg berre i ein tabell på ei eige side, og la link til sida i navbar-en. I tabellen viste eg registreringsnummeret, modellen, merket, antall dekk og ei handling kolonne. I handling kolonna sette eg ein knapp som førte til ei eige side eg sette opp som viste all dataen på den spesifikke bilen. På denne bil detaljer sida, blir all dataen på bilen vist i ein tabell, og under er knappane til å redigera bilen og for å sletta bilen. Dette føltest som ein intuitiv måte å gjera da på. På sida for redigering av bilane, ser det ganske likt ut som på sida ein legg til ny bil, kun at dataen på bilen er allereie i “input” felta og ingen felt for registreringsnummer.

På framsida der ein kunne søke på registreringsnummer, endra eg frå at kun dekk blei vist, til at all dataen på bilen blir vist. Under dataen, la eg også til ein knapp for å gå til bil detaljer sida, og ein knapp som går direkte til redigering av bil data.

Eg oppdaterte også til nyaste versjon av Bootstrap, for å få ein litt betre “flow” i layouten, og litt meir moderne “look”. Tok overall i meir bruk Bootstrap elementer. La også til nokre knappar på framsida for lett tilgang til dei essensielle funksjonane i applikasjonen.

Framtidige forbetringar er blant anna det å kunna legga til fleire innstillingar, fleire måtar å søka opp bilar i databasen på, filtrering av listen over alle bilane, og sjølvsagt meir data ein kan lagra på registreringar som f.eks eigaropplysningar. Det er mykje potensiale for ein applikasjon slik som detta og det er absolutt mykje ein kan legga til for å gjera brukarvennleggeiten betre, spesielt når verkstadane har ein stor database med mykje data for eksempel. Det viktigaste er å ha ein applikasjon som er enkel og intuitiv i bruk, gjer meining og har alt det verkstadane treng, pluss litt meir.

Det var absolutt krevjande å setta seg inn i noko så relativt nytt som detta, med eg føler eg fekk løyst oppgåva på ein god måte.

#### - Magnar Lussand Selheim
