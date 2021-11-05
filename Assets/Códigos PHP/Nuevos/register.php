<?php

  $con = mysqli_connect('remotemysql.com', 'VCcUZBpNf4','biSiA4evNg', 'VCcUZBpNf4');

  if(mysqli_connect_errno()){
    echo mysqli_connect_errno();
    exit();
  }

  
  $email = $_POST["email"];
  $password = $_POST["password"];
  $birthday = $_POST["birthday"];
  $lastnames = $_POST["lastnames"];
  $names = $_POST["names"];
  $username = $_POST["username"];
  $id_user = $_POST["id"];

  $inserquery1 = "INSERT INTO user_login (id_user_login, is_active,password, username) VALUES ($id_user,1,'" . $password . "', '" . $username . "');";

  $inserquery2 = "INSERT INTO guardian (id_guardian, email, user_login_id_user_login, birthday, last_names, names) VALUES ($id_user, '" . $email . "','" . $id_user . "','" . $birthday . "','" . $lastnames . "','" . $names . "');";


  mysqli_query($con, $inserquery1) or die("Error en la primera query" );
  mysqli_query($con, $inserquery2) or die("Error en la segunda query" );
  

  $data = array('done' => true, 'message' => 'Registro realizado con exito');
    Header('Content-Type: application/json');
    echo json_encode($data);

  
?>
