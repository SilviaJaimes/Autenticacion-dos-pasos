const urlVerify = "http://localhost:5075/api/user/verify";
const urlQr = "http://localhost:5075/api/user/qr";
const urlRegister = "http://localhost:5075/api/User/register";
const headers = new Headers({ 'Content-Type': 'application/json' });

const botonRegister = document.getElementById('botonRegister');

botonRegister.addEventListener("click", function (e) {
    e.preventDefault();
    register();
});

async function register() {
    let inputUsuario = document.getElementById('username').value;
    let inputEmail = document.getElementById('email').value;
    let inputPassword = document.getElementById('password').value;
    console.log(inputUsuario);
    console.log(inputEmail);
    console.log(inputPassword);

    let data = {
        "usuario": inputUsuario,
        "email": inputEmail,
        "password": inputPassword
    }
    console.log("entrooooooo");
    const config = {
        method: 'POST', 
        headers: headers,
        body: JSON.stringify(data)
    };
    try {
        const response = await fetch(`${urlRegister}`, config);
    
        if (response.status === 200) {
            console.log("El registro fue exitoso.")
        } else {
            console.error("La solicitud no fue exitosa. Estado:", response.status);
        }
    } catch (error) {
        console.error("Error de red: ", error);
    }
}