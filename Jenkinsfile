pipeline {
    agent any

    stages {

        stage('Build .NET') {
            steps {
                sh 'dotnet build'
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish -c Release'
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t devops-lab-api .'
            }
        }

    }
}