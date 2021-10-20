<?php
include_once "config.php";
include "validate.php";

$userName = $_POST ['userName'];
$pass = hash ("sha256" , $_POST ['pass']);

$sql = "SELECT user From usuarios WHERE user = '$userName' AND password = '$pass'";
$result = $pdo->query ($sql);

if ($result->rowCount() > 0){
  $data = array('done' => true, 'message' => "Logueado con exito");
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}else{
  $data = array('done' => false, 'message' => "Error 555: Algunos datos son incorrectos");
  Header('Content-Type: application/json');
  echo json_encode($data);
  exit();
}
?>
