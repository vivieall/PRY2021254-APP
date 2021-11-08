<?php

  $con = mysqli_connect('remotemysql.com', 'VCcUZBpNf4','biSiA4evNg', 'VCcUZBpNf4');

  if(mysqli_connect_errno()){
    echo mysqli_connect_errno();
    exit();
  }
  Header('Content-Type: application/json');
  
  $email = $_POST["email"];
  $password = $_POST["password"];
  $birthday = $_POST["birthday"];
  $lastnames = $_POST["lastnames"];
  $names = $_POST["names"];
  $username = $_POST["username"];

  $inserquery1 = "INSERT INTO user_login (is_active,password, username) VALUES (1,'" . $password . "', '" . $username . "');";
  $registroFallido1 = array('done' => false, 'message' => 'Registro fallido, error en la primera query');
  mysqli_query($con, $inserquery1) or die(json_encode($registroFallido1));


  $id = mysqli_insert_id($con);
  $inserquery2 = "INSERT INTO guardian (id_guardian, email, user_login_id_user_login, birthday, last_names, names) VALUES ( $id , '" . $email . "', $id ,'" . $birthday . "','" . $lastnames . "','" . $names . "');";
  $registroFallido2 = array('done' => false, 'message' => 'Registro fallido, error en la segunda query');
  mysqli_query($con, $inserquery2) or die(json_encode($registroFallido2));

  

  $data = array('done' => true, 'message' => 'Registro realizado con exito');
  
  echo json_encode($data);

  
?>
