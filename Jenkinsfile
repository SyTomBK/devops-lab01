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

        stage('Docker Build') {
            steps {
                sh 'docker build -t devops-lab-api .'
            }
        }

    }
}