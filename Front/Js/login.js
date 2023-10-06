const urlVerify = "http://localhost:5075/api/user/verify";
const urlQr = "http://localhost:5075/api/user/qr";
const headers = new Headers({ 'Content-Type': 'application/json' });

const botonLogin = document.getElementById('botonLogin');

botonLogin.addEventListener("click", function (e) {
    e.preventDefault();
    verificacionEmail();
});

async function verificacionEmail() {
    let inputUsuario = document.getElementById('username').value;
    let inputPassword = document.getElementById('password').value;
    console.log(inputUsuario);
    console.log(inputPassword);

    let data = {
        "usuario": inputUsuario,
        "password": inputPassword
    }
    console.log("entrooooooo");
    const config = {
        method: 'POST', 
        headers: headers,
        body: JSON.stringify(data)
    };
    try {
        const response = await fetch(`${urlQr}`, config);
    
        if (response.status === 200) {
            window.location.href = '../Front/Html/login2paso.html?usuario='+inputUsuario; 
        } else {
            console.error("La solicitud no fue exitosa. Estado:", response.status);
        }
    } catch (error) {
        console.error("Error de red: ", error);
    }
    
}
