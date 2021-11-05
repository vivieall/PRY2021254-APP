<?php

  $con = mysqli_connect('remotemysql.com', 'VCcUZBpNf4','biSiA4evNg', 'VCcUZBpNf4');

  if(mysqli_connect_errno()){
    echo mysqli_connect_errno();
    exit();
  }

  
  $password = $_POST["password"];
  $username = $_POST["username"];


  $query1 = "SELECT * FROM user_login WHERE is_active = 1 AND username = '" . $username . "' AND password = '" . $password . "'";


  $result = mysqli_query($con, $query1) or die("Error en la query" );


  if(mysqli_num_rows($result) > 0){
    $data = array('done' => true, 'message' => 'Logeo realizado con exito');
    Header('Content-Type: application/json');
    echo json_encode($data);
  }
  else{
    $data = array('done' => false, 'message' => 'No se pudo logear');
    Header('Content-Type: application/json');
    echo json_encode($data);
  }



  
?>
