pipeline {
    agent any

    stages {

        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --no-restore'
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