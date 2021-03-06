## 參考用，並未依此實作所有項目

#### 會員登入 (`anonymous`)

1.  使用者點選登入功能後，畫面跳轉到`登入表單頁面`。
1.  輸入帳號、密碼後透過`Post`送出資料到後端驗證。
1.  驗證成功後設置`Session`並導向首頁；失敗則提示錯誤原因。

> 輸入驗證 參照<a href="#memberValidateRule">會員資料驗證規則</a>

#### 註冊會員 (`anonymous`)

1.  使用者點選註冊功能後，畫面跳轉到`新增會員頁面`。
1.  填寫各項必填資料後，透過`Post`送出資料到後端驗證。
1.  驗證成功將使用者資料寫入 DB，狀態設為登入並導向首頁；失敗提示錯誤原因。

> 密碼欄位不存明碼，需另外做 `Hash`  
> 輸入驗證 參照<a href="#memberValidateRule">會員資料驗證規則</a>

<a id="updateMember"></a>

#### 修改會員資料 (`Member`, `Admin`)

1.  使用者點選修改會員資料後，畫面跳轉到`修改會員資料頁面`。
1.  系統接收要被修改資料的使用者`Id`，並判斷目前使用者是否有權限修改該會員的資料，若無權限則返回首頁。
1.  系統將表單預設值設為目標使用者資料，但密碼欄位留白，若使用者不需修改密碼就不用填寫。
1.  填寫完畢透過`Post`送出資料到後端驗證。
1.  通過會員資料驗證後修改會員資料，同時返回首頁；驗證失敗則提示錯誤訊息。

> 須注意後端要額外判斷是否需要驗證密碼、修改密碼欄位。

#### 會員登出 (`Member`, `Admin`)

1.  使用者點選登出。
1.  系統清除 `Session`，並將畫面導回首頁。

# 瀏覽文章 (`anonymous`, `Member`, `Admin`)

1.  系統讀取使用者登入狀態、搜尋條件、分頁、排序作為篩選資料的依據。
1.  系統取得文章資料並呈現於前端`瀏覽文章頁面(首頁)`

#### 新增文章 (`Member`, `Admin`)

1.  使用者點選`New Post`，畫面跳轉到`發表文章頁面`。
1.  使用者寫完文章點選`新增`發表文章。
1.  系統將文章資料存入資料庫，畫面跳轉到首頁。

# 修改文章 (`Member`, `Admin`)

1.  使用者點選修改按鈕，畫面跳轉到`修改文章頁面`。
1.  系統讀取該討論串資料，包含文章主體及回文資料，送到前端呈現
1.  在原先文章主體的部分，提供文字框及原先的內容，使用者編輯完畢後可點選送出修改。
1.  系統將文章資料存入資料庫，畫面跳轉到首頁。

# 讀取回覆 (`anonymous`, `Member`, `Admin`)

1.  使用者點選某篇文章的`Read More`按鈕
1.  從前端透過`Ajax`向後端 API 取得該文章的回文資料。
1.  前端收到資料後呈現
    > Read More 應先判斷挖的洞裏面沒資料才呼。

# 新增回覆 (`Member`, `Admin`)

1.  使用者點選文章或底下回文的`Reply`進行回復，畫面跳轉到`新增回覆頁面`。
1.  系統讀取該討論串資料，包含文章主體及回文資料，送到前端呈現
1.  在資料最下方增加一個回文的表單，使用者填寫完畢後可點選送出
1.  系統將回覆儲存後，頁面跳轉到首頁。

# 修改回覆 (`Member`, `Admin`)

1.  使用者點選修改按鈕，畫面跳轉到`修改回覆頁面`。
1.  系統讀取該討論串資料，包含文章主體及回文資料，送到前端呈現
1.  在原先回覆的部分，提供文字框及原先的內容，使用者編輯完畢後可點選送出修改。
1.  系統將文章資料存入資料庫，畫面跳轉到首頁。

# 刪除 Post、回覆 (`Member`, `Admin`)

1.  使用者點選刪除按鈕，將請求透過`Ajax`拋到後端。
1.  系統驗證若為合法請求則將該文章或回覆的`IsDel`改為 True
1.  系統回應前端執行結果
1.  前端接收判斷若為成功，則透過 js 將前端相關文章資料移除；若刪除失敗則不動作。

#### 後台管理會員 (`Admin`)

1.  管理者點選`後台`，則畫面跳轉到`後台管理介面`。
1.  系統驗證使用者身份是否為管理者，不符合身份則跳轉至首頁。
    > 修改其他會員資料請參照<a href="#updateMember">修改會員資料</a>

# 變更會員狀態 (`Admin`)

1.  管理者變更某會員的狀態，透過 ajax 將資料送到後端。
1.  系統驗證若為合法請求則將該會員狀態依照請求處理。
1.  系統回應前端執行結果
1.  前端判斷若成功則將 CheckBox 依照結果設置；失敗就顯示錯誤訊息。

<a id="memberValidateRule"></a>

# 會員資料驗證規則

#### 通用規則

1.  Email 帳號須包含小老鼠符號
1.  密碼長度需大於 4 碼

#### 後端驗證

1.  若是修改會員資料，需要判斷密碼不允許與前兩次密碼相同
1.  需判斷是否已存在相同帳號使用者
