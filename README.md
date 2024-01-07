
# Clean Architecture Starter Template (.NET)

This repository contains a starter template for building applications using Clean Architecture principles in ASP.NET.

## Functionalities

 - ✅ Authentication
 - ✅ Options pattern
 - 🔧 Repository pattern - WiP [@repository](https://github.com/chyla95/Clean-Architecture-Starter-Template/tree/repository) branch
 - ❌ Result pattern based exception handling

## Generating Keys with OpenSSL

### Generate Private Key

Run this command to create an RSA **private key** encrypted with AES256:

    openssl genpkey -algorithm RSA -out private_key.pem -aes256
    
### Derive Public Key

Generate the corresponding **public key** from the private key:

    openssl rsa -pubout -in private_key.pem -out public_key.pem
