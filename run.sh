#!/bin/bash
trap 'echo "Terminando script..." && kill 0' EXIT
echo "Inicializando la aplicacion"

# Iniciar Backend (.NET 8.0)
cd backend

dotnet restore
dotnet build
dotnet run &
cd ..

# Iniciar Frontend (Vue.js)
cd frontend

npm install
npm run dev &
cd ..
 
wait
