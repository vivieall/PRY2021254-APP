<?php

  $con = mysqli_connect('remotemysql.com', 'VCcUZBpNf4','biSiA4evNg', 'VCcUZBpNf4');

  if(mysqli_connect_errno()) {
    echo mysqli_connect_errno();
    exit();
  }
  Header('Content-Type: application/json');
  
  $guardian = $_POST["guardian_id"];
  $gender = $_POST["gender"];
  $birthday = $_POST["birthday"];
  $lastnames = $_POST["lastnames"];
  $names = $_POST["names"];
  $asdlevel = $_POST["asdlevel"];
  $symptoms = $_POST["symptoms"];
  
  $inserquery1 = "INSERT INTO child (asd_level, avatar, birthday, gender, last_names, names, guardian_id_guardian) VALUES ('" . $asdlevel . "', 'test','" . $birthday . "', '" . $gender . "', '" . $lastnames . "', '" . $names . "', $guardian);";
  
  $registroFallido1 = array('done' => false, 'message' => "No se pudo registrar, la primera query falló");
  mysqli_query($con, $inserquery1) or die(json_encode($registroFallido1));
  $id = mysqli_insert_id($con);


  
  for ($i = 1; $i <= strlen($symptoms); $i++){
    if($symptoms[$i-1] == '1') {
      $inserquery2 = "INSERT INTO symptom_children (symptoms_id_symptom, children_id_child) VALUES ($i , $id);";
      $registroFallido2 = array('done' => false, 'message' => $inserquery2);
      mysqli_query($con, $inserquery2) or die(json_encode($registroFallido2));
      
    }
}




  $data = array('done' => true, 'message' => "hola");
  
  echo json_encode($data);

  
?>
