echo "🚀 Stopping existing containers..."
docker compose --env-file .env.default down

echo "🔄 Building and starting containers with Docker🐋"
docker compose --env-file .env.default up --build -d

echo "✅ Application is running! Access it at:"
echo "🌍 Frontend: http://localhost:5173"
echo "🔧 Backend API: http://localhost:5169/swagger/index.html"