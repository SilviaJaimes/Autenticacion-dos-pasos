const urlVerify = "http://localhost:5075/api/user/verify";
const urlQr = "http://localhost:5075/api/user/qr";
const headers = new Headers({ 'Content-Type': 'application/json' });

const botonCodigo = document.getElementById('botonCodigo');

botonCodigo.addEventListener("click", function (e) {
    e.preventDefault();
    codigoVerificacion();
});

async function codigoVerificacion() {
    let inputUsuario = document.getElementById('username').value;
    let inputCodigo = document.getElementById('Codigo').value;
    console.log(inputUsuario);
    console.log(inputCodigo);

    let data = {
        "usuario": inputUsuario,
        "Code": inputCodigo
    }
    console.log("entrooooooo");
    const config = {
        method: 'POST', 
        headers: headers,
        body: JSON.stringify(data)
    };
    try {
        const response = await fetch(`${urlVerify}`, config);
    
        if (response.status === 200) {
            window.location.href = '../Html/home.html'; 
        } else {
            console.error("La solicitud no fue exitosa. Estado:", response.status);
        }
    } catch (error) {
        console.error("Error de red: ", error);
    }
}
