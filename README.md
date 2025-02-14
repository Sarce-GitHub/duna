# duna

## Proprietà di configurazione

### Info app e Connessioni

- **BaseVers:** stringa presente prima del numero di versione (es. BETA 1.0.0.0) 
- **BaseTitle:** titolo visualizzato nella master page, inclusa da tutte le viste
- **Passphrase:** chiave di encryption
- **SQL_IP:** host del DB SQL
- **SQL_Catalog:** nome del DB SQL
- **SQL_User:** utente per connettersi al DB SQL
- **SQL_Psw:** password di connessione al DB SQL
- **WS_Url:** stringa di connessione ai Web Services Sage
- **WS_Pool:** pool di deploy dei Web Services Sage, indica anche lo Schema corrsispondente sul DB SQL
- **WS_User:** utente per connettersi ai Web Services Sage
- **WS_Pws:** password per connettersi ai Web Services Sage
- **WS_Lan:** lingua di riferimento per i Web Services Sage

### Funzionalità e pulsanti

- **Etic_Split:** carattere di split tra le varie componenti del barcode (articolo/lotto/sottolotto, *ITMREF_0/LOT_0/SLO_0*)
- **BarCode_Only** il valore True nasconde i campi di "Ricerca generica" nelle pagine in cui sono presenti, lasciando disponibile solo la ricerca per barcode 
- **RIC_TipoUbic:** ubicazione di default dell'area RICEVIMENTO
- **PRESPED_Ubic:** ubicazione di default per il materiale in prespedizione (*LOCTYP_0=PRE*)
- **SPED_Ubic:** ubicazione di default per il materiale in spedizione (*LOCTYP_0=SPE*)
- **Abil_SEAKEY:** abilita la gestione della Seakey: questa verrà mostrata a video in alcune viste
- **Abil_SEAKEY_LOT:** lotto di default indicato per la SEAKEY qualora ne sia attiva la gestione
- **Abil_MAG_EntrDiv:** abilita il pulsante ENTRATA DIVERSA (attualmente non aggiornata rispetto alla versione di Beccati)
- **SPED_BollaPrep:** il valore True indica che la preparazione degli ordini della sezione SPEDIZIONI è gestita a partire dalla prebolla e non dall'ordine
- **Manage_NAV:** il valore True abilita i pulsanti CARICO NAVETTA e SCARICO NAVETTA nell'area MAGAZZINO (definiscesi navetta un ubicazione con *LOCTYP_0=NAV*)
- **Manage_DISIMPEGNO:** il valore True abilita la funzionalità DISIMPEGNO PALLET nell'area SPEDIZIONI
- **Manage_PRESPED:** il valore True abilita la gestione dell'area di PRESPEDIZIONE, coi relativi pulsanti CARICO PRESPEDIZIONE e PREPARA PALLET
- **Manage_MAG_PALLET** il valore True abilita la gestione nell'area magazzino delle operazioni relative allo spostamento, alla creazione e al disassemblaggio dei pallet
- **CONS_MATERIALI_TSICOD_TO_CHECK**: specifica il valore del campo TSICOD_0 da controllare nella sezione CONSUMO MATERIALI; una corrispondenza cambierà la gestione delle quantità e dell'unità di misura da STU a PCU 
- **DICHIARAZIONE_PRODUZIONE_TOLERANCE_PERCENTAGE**: indica la tolleranza accettata all'inserimento di una quantità nella sezione Dichiarazione di Produzione;
- **DICHIARAZIONE_PRODUZIONE_CHECK_TOLERANCE**: il valore True fa sì che al caricamento ed alla conferma di una quantità nella sezione Dichiarazione di Produzione venga controllato che la quantità a schermo sia all'interno della tolleranza percentuale definita con la costante *DICHIARAZIONE_PRODUZIONE_TOLERANCE_PERCENTAGE*;
### Invio mail 

- **MAIL_SMTP:** host SMTP
- **MAIL_SMTP_PORT:** porta SMTP
- **MAIL_SMTP_USER:** utente SMTP
- **MAIL_SMTP_PASSWORD:** password SMTP
- **MAIL_FROM:** mittente mail
- **MAIL_TO_DDT:** destinatario mail. In caso di indirizzi multipli separarli con ';' (es. test@mail.com;prova@mail.it)
