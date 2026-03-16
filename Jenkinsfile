pipeline {
    agent any

    stages {

        stage('Restore') {
            steps {
                sh '''
                docker run --rm \
                -v $WORKSPACE:/src \
                -w /src/lab01-hello-api \
                mcr.microsoft.com/dotnet/sdk:8.0 \
                dotnet restore lab01-hello-api.csproj
                '''
            }
        }

        stage('Build') {
            steps {
                sh '''
                docker run --rm \
                -v $WORKSPACE:/src \
                -w /src/lab01-hello-api \
                mcr.microsoft.com/dotnet/sdk:8.0 \
                dotnet build lab01-hello-api.csproj --no-restore
                '''
            }
        }

        stage('Publish') {
            steps {
                sh '''
                docker run --rm \
                -v $WORKSPACE:/src \
                -w /src/lab01-hello-api \
                mcr.microsoft.com/dotnet/sdk:8.0 \
                dotnet publish lab01-hello-api.csproj -c Release -o /src/publish
                '''
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t devops-lab-api .'
            }
        }

    }
}