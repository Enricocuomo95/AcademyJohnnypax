
let semaforo = 1;

setInterval(() =>{
    let elencoBacchetta = localStorage.getItem("elencoBacchetta") != null ? 
                            JSON.parse( localStorage.getItem("elencoBacchetta") ) : [];
    var rowCount = $('#corpo-tabella tr').length;
        if(semaforo == 1)
            if(elencoBacchetta.length != rowCount)
                    stampa(elencoBacchetta);
}, 1000);





function init(){
    //creazione elenco se non esiste
    let elencoBacchetta = localStorage.getItem("elencoBacchetta") != null ? 
                            JSON.parse( localStorage.getItem("elencoBacchetta") ) : [];
    let varCodice = document.getElementById("input-codice").value;
    let varMateriale = document.getElementById("select-materiale").value;
    let varNucleo = document.getElementById("select-nucleo").value;
    let varLunghezza = document.getElementById("input-lunghezza").value;
    let varMago = document.getElementById("input-proprietario").value;
    let varCasata = document.getElementById("select-casata").value;
    let varFoto = document.getElementById("url-foto").value;

    if(varMago == ""){
        window.alert("La bacchetta deve avere un proprietario!");
        return;
    }

    let stud = {
        codice: varCodice,
        materiale: varMateriale,
        nucleo: varNucleo,
        lunghezza: varLunghezza,
        mago: varMago,
        casata: varCasata,
        foto: varFoto
    }

    let StringCodici = [];

    for(let [idx, item] of elencoBacchetta.entries())
        StringCodici.push(item.codice);


    if(StringCodici.includes(varCodice))
    {
        window.alert("Hai gia inserito questa bacchetta o il tuo codice non è valido");
        return;
    }

    elencoBacchetta.push(stud);
    //STAMPO ARRAY
    semaforo = 0;
    stampa(elencoBacchetta);  
}  


function stampa(elencoBacchetta){
    let contenuto = "";

    for(let [idx, item] of elencoBacchetta.entries())
    {
        var img = `img/${item.foto}`;
        img = img  + ".jpg";
        contenuto += `
            <tr>
                <td>${item.codice}</td>
                <td>${item.materiale}</td>
                <td>${item.nucleo}</td>
                <td>${item.lunghezza}</td>
                <td>${item.mago}</td>
                <td>${item.casata ? item.casata : "n.d."}</td>
                <td></td>
                <td> <img src=${img} alt="Foto non valida" Width=100; Height=100;/></td>
                <td>
                    <button type="button" class="btn btn-danger" onclick="elimina(${idx})">Elimina</button>
                </td>
            </tr>
        `;
    }  

    document.getElementById("corpo-tabella").innerHTML = contenuto;
    localStorage.setItem("elencoBacchetta", JSON.stringify(elencoBacchetta));
    setInterval(() => {
        semaforo = 1;
    }, 600);
}



function elimina(idx){
    let elencoBacchetta = JSON.parse(localStorage.getItem("elencoBacchetta"));
    elencoBacchetta.splice(idx,1);
    localStorage.setItem("elencoBacchetta", JSON.stringify(elencoBacchetta));
}