docker build -t rubeha/homeservice_apigateway:latest -f ApiGateway/Dockerfile .
docker build -t rubeha/homeservice_iacapi:latest -f Services/IAC/IAC.API/Dockerfile .
docker build -t rubeha/homeservice_productapi:latest -f Services/Products/Products.API/Dockerfile .
docker build -t rubeha/homeservice_shoppingapi:latest -f Services/Shopping/Shopping.API/Dockerfile .
docker build -t rubeha/homeservice_contractapi:latest -f Services/Contracts/Contracts.API/Dockerfile .
docker build -t rubeha/homeservice_employeeapi:latest -f Services/Employees/Presentation/Employees.API/Dockerfile .
docker build -t rubeha/homeservice_installationapi:latest -f Services/Installation/Presentation/Installations.API/Dockerfile .

docker push rubeha/homeservice_apigateway:latest
docker push rubeha/homeservice_iacapi:latest
docker push rubeha/homeservice_productapi:latest
docker push rubeha/homeservice_shoppingapi:latest
docker push rubeha/homeservice_contractapi:latest
docker push rubeha/homeservice_employeeapi:latest
docker push rubeha/homeservice_installationapi:latest