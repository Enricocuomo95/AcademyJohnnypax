#Il sistema di gestione impiegati Creare un sistema di gestione impiegati in ASP Web. Ogni impiegato è caratterizzato da: 
1 Matricola 1 Nome 1 Cognome 1 Data Nascita 1 Ruolo 1 Reparto (da UNA TABELLA SUL DB) 
1 Indirizzo di residenza 2 Città di residenza (da UNA TABELLA SUL DB) 2 Provincia di residenza (da UNA TABELLA SUL DB) 
a. Sviluppare tutti i punti 1
b. Sviluppare i punti 2 (possibilmente con AJAX o Fetch) 
c. Sviluppare un campo di ricerca di impiegati per matricola
d. HARD: Collegare con una relazione, il popolamento della selectbox di Città deve essere collegato alla selezione della provincia (es. Se seleziono AQ, all'interno della select di Città devono essere presenti solo gli elementi che hanno la provincia AQ).

In questo applicativo two-tier abbiamo delle table che non sono legate tra loro. Infatti qui non è presente uno schema ER


// Effettuiamo la richiesta AJAX utilizzando fetch
fetch('https://example.com/GetCitiesByProvince?keyword=r', {
    method: 'GET'
})
.then(function(response) {
    // Controlla se la richiesta è stata eseguita con successo
    if (!response.ok) {
        throw new Error('Errore nella richiesta: ' + response.statusText);
    }
    // Converte la risposta in formato JSON
    return response.json();
})
.then(function(data) {
    // Elabora la risposta JSON
    console.log(data);
})
.catch(function(error) {
    // Gestisci gli errori
    console.error('Si è verificato un errore durante la richiesta:', error);
});

