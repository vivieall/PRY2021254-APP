<?php
include "config.php";
include "validate.php";

$userName = $_POST ['userName'];
$email = $_POST ['email'];
$pass = hash ("sha256" , $_POST ['pass']);

$sql = "SELECT user From usuarios WHERE user = '$userName'";
$result = $pdo->query($sql);

if ($result->rowCount() > 0){
  $data = array('done' => false, 'message' => 'Error 254: Usuario en uso');
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}else{
  $sqla = "SELECT email From usuarios WHERE email = '$email'";
  $result = $pdo->query($sqla);

  if ($result->rowCount() > 0){
    $data = array('done' => false, 'message' => 'Error 111: Email existente');
    Header('Content-Type: application/json');
    echo json_encode($data);
    exit();
  }else{
    $sql = "INSERT INTO usuarios SET user = '$userName', email = '$email', password = '$pass'";
    $pdo->query($sql);

    $data = array('done' => true, 'message' => 'Cuenta creada con exito');
    Header('Content-Type: application/json');
    echo json_encode($data);
    exit();
  }
}
?>
