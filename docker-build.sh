docker build -t rubeha/homeservice_apigateway:latest -f ApiGateway/Dockerfile .
docker build -t rubeha/homeservice_iacapi:latest -f Services/IAC/IAC.API/Dockerfile .
docker build -t rubeha/homeservice_productapi:latest -f Services/Products/Products.API/Dockerfile .
docker build -t rubeha/homeservice_shoppingapi:latest -f Services/Shopping/Shopping.API/Dockerfile .
docker build -t rubeha/homeservice_customerapi:latest -f Services/Customers/Customers.API/Dockerfile .

docker push rubeha/homeservice_apigateway:latest
docker push rubeha/homeservice_iacapi:latest
docker push rubeha/homeservice_productapi:latest
docker push rubeha/homeservice_shoppingapi:latest
docker push rubeha/homeservice_customerapi:latest