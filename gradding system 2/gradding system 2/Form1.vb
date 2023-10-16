Imports grading_system_2.Form1
Imports System.IO

Public Class Form1
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click





        Dim answerKey As String = File.ReadAllText("C:\Users\Administrator\source\repos\gradding system 2\Answer_key.txt")
        TextBox1.Text = answerKey



        Dim studentRecords As New List(Of StudentRecord)()
        For Each line As String In File.ReadLines("C:\Users\Administrator\source\repos\gradding system 2\student.txt")
            Dim parts() As String = line.Split(","c)
            If parts.Length = 2 Then
                Dim student As New StudentRecord With {.Name = parts(0), .Answers = parts(1)}
                studentRecords.Add(student)
            End If
        Next

        Dim totalStudents As Integer = 0
        Dim totalCorrectAnswers As Integer = 0

        For Each student As StudentRecord In studentRecords
            Dim correctAnswers As Integer = CompareAnswers(student.Answers, answerKey)
            Dim letterGrade As Char = CalculateLetterGrade(correctAnswers)

            ListBox1.Items.Add($"{student.Name},    {correctAnswers},   {letterGrade}")

            totalStudents += 1
            totalCorrectAnswers += correctAnswers
        Next

        ListBox1.Items.Add($"Total Students: {totalStudents}, Total Correct Answers: {totalCorrectAnswers}")

        Dim averageCorrectAnswer As Double = If(totalStudents > 0, totalCorrectAnswers / totalStudents, 0)
        TextBox2.Text = $" {averageCorrectAnswer:F2}"





    End Sub

    Private Function CompareAnswers(studentAnswers As String, answerKey As String) As Integer
        If studentAnswers.Length <> answerKey.Length Then
            Return 0 ' Return 0 for invalid answers
        End If

        Dim correctAnswers As Integer = 0

        For i As Integer = 0 To studentAnswers.Length - 1
            If studentAnswers(i) = answerKey(i) Then
                correctAnswers += 1
            End If
        Next

        Return correctAnswers
    End Function

    Private Function CalculateLetterGrade(correctAnswers As Integer) As Char
        If correctAnswers >= 9 Then
            Return "A"c
        ElseIf correctAnswers >= 8 Then
            Return "B"c
        ElseIf correctAnswers >= 7 Then
            Return "C"c
        ElseIf correctAnswers >= 6 Then
            Return "D"c
        Else
            Return "F"c
        End If
    End Function

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub
End Class

Public Class StudentRecord
    Public Property Name As String
    Public Property Answers As String
End Class





Public Class Student
    Public Property Name As String
    Public Property CorrectCount As Integer
    Public Property LetterGrade As Char

    Public Sub New(name As String, correctCount As Integer, letterGrade As Char)
        Me.Name = name
        Me.CorrectCount = correctCount
        Me.LetterGrade = letterGrade
    End Sub
End Class
