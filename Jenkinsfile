pipeline {
    agent any

    stages {

        stage('Restore') {
            steps {
                sh 'docker run --rm -v $PWD:/src -w /src mcr.microsoft.com/dotnet/sdk:8.0 dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'docker run --rm -v $PWD:/src -w /src mcr.microsoft.com/dotnet/sdk:8.0 dotnet build --no-restore'
            }
        }

        stage('Publish') {
            steps {
                sh 'docker run --rm -v $PWD:/src -w /src mcr.microsoft.com/dotnet/sdk:8.0 dotnet publish -c Release'
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t devops-lab-api .'
            }
        }

    }
}