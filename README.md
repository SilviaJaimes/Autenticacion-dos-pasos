# Sistema de Autenticación con Doble Factor usando QR en correo electrónico

Este proyecto proporciona una API que permite a los usuarios registrarse, autenticarse y utilizar la autenticación de dos factores con códigos QR en correo electrónico.

## Características 🌟

- Registro de usuarios.
- Autenticación con usuario y contraseña.
- Autenticación de dos factores usando códigos QR.
- Envío del código QR por correo electrónico.
- Verificación del código de autenticación.
- CRUD completo para usuarios.

## Uso 🕹

Una vez que el proyecto esté en marcha, puedes acceder a los diferentes endpoints disponibles:

## 1. Registro de Usuarios

**Endpoint**: `/register`

**Método**: `POST`

**Payload**:

json
`{
    "Usuario": "<nombre_de_usuario>",
    "Password": "<contraseña>",
    "Email": "<correo_electronico>"
}`

Este endpoint permite a los usuarios registrarse en el sistema.

## 2. Generación de Código QR:

**Endpoint**: `/QR`

**Método**: `POST`

**Payload**:

`{
    "Usuario": "<nombre_de_usuario>",
    "Password": "<contraseña>"
}`

Al proporcionar el nombre de usuario y contraseña correctos, el sistema enviará un código QR al correo electrónico registrado del usuario.

## 3. Verificación de 2FA:

**Endpoint**: `/Verify`

**Método**: `POST`

**Payload**:

`{
    "Code": "<codigo_de_verificacion>",
    "Usuario": "<nombre_de_usuario>"
}`

Una vez que el usuario escanea el código QR con una aplicación de autenticación (como Google Authenticator), debe ingresar el código generado por la aplicación para verificar su autenticidad.

**Otros Endpoints**

Obtener Todos los Usuarios: GET `/`

Obtener Usuario por ID: GET `/{id}`

Actualizar Usuario: PUT `/{id}`

Eliminar Usuario: DELETE `/{id}`

## Desarrollo ⌨️
Este proyecto utiliza varias tecnologías y patrones, incluidos:

Entity Framework Core para la ORM.

Patrón Repository y Unit of Work para la gestión de datos.

TwoFactorAuthNet para la generación de códigos QR y verificación.

AutoMapper para el mapeo entre entidades y DTOs.

SMTP para el envío de correos electrónicos.

## Agradecimientos 🎁

A todas las librerías y herramientas utilizadas en este proyecto.

A ti, por considerar el uso de este sistema.

⌨️ con ❤️ por Silvia y Owen 😊
