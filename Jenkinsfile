pipeline {
    agent any

    stages {

        stage('Debug Workspace') {
            steps {
                sh 'pwd'
                sh 'ls -la'
            }
        }

        stage('Debug In Container') {
            steps {
                sh '''
                docker run --rm \
                -v $WORKSPACE:/src \
                alpine \
                sh -c "ls -la /src"
                '''
            }
        }

        stage('Restore') {
            steps {
                sh '''
                docker run --rm \
                -v $WORKSPACE:/src \
                -w /src \
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
                -w /src \
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
                -w /src \
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