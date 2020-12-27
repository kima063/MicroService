USE ratingservicedb;

DROP TABLE rating;
CREATE TABLE rating (
    rating_uuid char(36) NULL,
    productId varchar(36) NULL,
    rating INT ,
    raterId varchar(36) NOT NULL
);
DROP TRIGGER before_insert_rating;
CREATE TRIGGER before_insert_rating
BEFORE INSERT ON rating
FOR EACH ROW
BEGIN
  IF new.rating_uuid IS NULL THEN
    SET new.rating_uuid = uuid();
  END IF;
END
;

DROP PROCEDURE IF EXISTS store_rating;

CREATE PROCEDURE store_rating(IN _productId varchar(36),IN _rating INT,IN _raterId varchar(36))
	BEGIN
	    DECLARE RATER INT DEFAULT 0;
	    SELECT COUNT(rating.raterId) into RATER
		FROM rating
		WHERE rating.raterId=_raterId
	    AND rating.productId=_productId;
	    IF RATER = 0 THEN
              INSERT INTO rating(productId,rating,raterId)
		      VALUES (_productId,_rating,_raterId);
        ELSE
	        UPDATE  rating
	            SET rating.rating=_rating
                WHERE rating.productId=_productId
	            AND rating.raterId=_raterId;
        end if;


-- 		 # SET _ratingID=LAST_INSERT_ID();
 	SELECT _productId AS 'productId';
	END;
--   CALL store_rating('2',4,'26');

USE ratingservicedb;
SELECT productId,AVG(rating) as average,COUNT(productId) as ratingCount FROM rating GROUP BY productId;